﻿"use strict";

var isStrict = (function () { return !this; })();

//utilizada para amazenar um valor temporário de cidade selecionada
var cidadeDoEstado;

function iconControl() {
    //$(".btn-edit").prepend("<i class='fa fa-pencil fa-fw'></i>"); //botão editar
    //$(".btn-inativar").prepend("<i class='fa fa-ban fa-fw'></i>"); //botão inativar
    //$(".btn-ativar").prepend("<i class='fa fa-power-off fa-fw'></i>"); //botão ativar

    $("#menu > li:nth-child(1) > a:first-child").prepend("<i class='fa fa-home fa-fw'></i>"); //Menu
    $("#menu > li:nth-child(3) > a:first-child").prepend("<i class='fa fa-archive fa-fw'></i>"); //Menu
    $("#menu > li:nth-child(5) > a:first-child").prepend("<i class='fa fa-edit fa-fw'></i>"); //Menu
    $("#menu > li:nth-child(7) > a:first-child").prepend("<i class='fa fa-shopping-cart fa-fw'></i>"); //Menu
    $("#menu > li:nth-child(9) > a:first-child").prepend("<i class='fa fa-cogs fa-fw'></i>"); //Menu
    $("#menu > li:nth-child(11) > a:first-child").prepend("<i class='fa fa-money fa-fw'></i>"); //Menu
    $("#menu > li:nth-child(13) > a:first-child").prepend("<i class='fa fa-tasks fa-fw'></i>"); //Menu
}

$(document).ready(function () {

    Globalize.culture('pt-BR');
    kendo.culture('pt-BR');
    
    $.ajaxSetup({
        // Disable caching of AJAX responses
        cache: false
    });
    
    // Foco no primeiro controle do formulário
    setTimeout(function () {
        // Espera até o kendoui transformar os controles
        $(":input:visible:enabled:not([readonly]):first").focus();
    }, 100);

    iconControl();

    $('ul.dropdown-menu [data-toggle=dropdown]').on('click', function (event) {
        event.preventDefault();
        event.stopPropagation();
        $(this).parent().siblings().removeClass('open');
        $(this).parent().toggleClass('open');
    });

    // Altera o foco para o próximo elemento ao pressionar ENTER.
    $(document).on('keydown', ':input:enabled:not(:button,:submit,textarea)', function (e) {
        if (e.keyCode == 13) // Enter key
        {
            var $inputs = $(':input:visible:enabled');
            var nextIndex = $inputs.index(this) + 1;

            // Quando alcançar o último, voltar para o primeiro elemento
            if (nextIndex > $inputs.length)
                nextIndex = 0;

            var input = $inputs.eq(nextIndex);

            if (input.length > 0)
                input.focus();

            e.preventDefault();
        }
    });

    // Evita que o usuário navege sem querer usando o Backspace.
    $(document).on('keydown', function (event) {
        var doPrevent = false;
        if (event.keyCode === 8) {
            var d = event.srcElement || event.target;
            if ($(d).is(":input")) {
                doPrevent = d.readOnly || d.disabled;
            }
            else {
                doPrevent = true;
            }
        }

        if (doPrevent) {
            event.preventDefault();
        }
    });

    // Converte o texto digitado em maiúscula
    $(document).on('keypress', 'textarea,input[type=text]', function (e) {
        var input = this;
        
        if (e.ctrlKey || e.altKey || e.metaKey || e.which < 32) return; // ignora teclas de controle

        if ($(this).data('inputmask')) return; // ignora quando existe máscara (jasny-bootstrap)

        if ($(this).hasClass('k-input')) return; // ignora quando é um campo do Kendo UI
        
        if (input.readOnly || input.disabled) return; // ignorar campos somente leitura e desabilitados

        var pressedKey = e.charCode == undefined ? e.keyCode : e.charCode;
        var str = String.fromCharCode(pressedKey);
        if (input.createTextRange && !isStrict) {
            window.event.keyCode = str.toUpperCase().charCodeAt(0);
        } else {
            var startpos = input.selectionStart;
            var endpos = input.selectionEnd;
            var oldvalue = input.value;

            window.setTimeout(function () {
                input.value = (oldvalue.slice(0, startpos) + str + oldvalue.slice(endpos)).toUpperCase();
                input.setSelectionRange(startpos + 1, startpos + 1);
            }, 0);
        }
    }).on('paste', 'textarea,input[type=text]', function () {
        var input = this;
        var startpos = input.selectionStart;
        var endpos = input.selectionEnd;
        var oldvalue = input.value;
        
        window.setTimeout(function () {
            input.value = input.value.toUpperCase();
            
            var pos = input.value.length - oldvalue.length + startpos + (endpos - startpos);
            if (pos < 0) pos = input.value.length;
                
            input.setSelectionRange(pos, pos);
        }, 0);
    }).on('blur', 'textarea,input[type=text]', function () {
        this.value = this.value.toUpperCase();
    });

    /* - Valida os formulários de cadastros - */
    $(document).on('click', 'input[type="submit"], button[type="submit"]', function () {
        var $button = $(this);
        var form = $button.closest("form");

        if ($button.attr('data-loading-text')) {
            $button.button('loading');
        }
        
        if (form.attr('novalidate') == 'novalidate') {
            $(form).validate().cancelSubmit = true;
            return true;
        }

        if (!form.valid()) {
            $button.button('reset');
            return false;
        }

        return true;
    });

    // Preenche o combo de cidade de acordo com o estado escolhido
    $(document).on('change', '#estado-option', function() {
        var cidades = $('#cidade-option');
        cidades.empty();
        var url = '/Comum/Endereco/Cidades/' + $(this).val();
        $.getJSON(url, function (result) {
            cidades.append($('<option value = ""/>').text('-- Selecione --'));
            if ($.isEmptyObject(result) == false) {
                $.each(result, function (index, item) {
                    cidades.append($('<option />').val(item.Id).text(item.Nome));
                });
                if (cidadeDoEstado != null) {
                    cidades.val(cidadeDoEstado);
                    cidadeDoEstado = null;
                }
            }
        }).fail(function(jqXhr, textStatus, errorThrown) {
            alert(errorThrown);
        });
    });

    // Formata a data no KendoUi DatePicker
    $(document).on('keyup', '.k-datepicker input', function () {
        formataData(this);
    });
    
    // Seleciona o texto nos textbox da telerik
    $(document).on('focus', '.k-input', function () {
        var input = $(this);
        setTimeout(function () { input.select(); });
    });
    
    // -- RELATÓRIOS --
    // Executa a pesquisa como POST no endereço atual, retorna um pdf e mostra na tela.
    // A estrutura do html deve ser igual para que possa funcionar corretamente
    $('#pesquisar-relatorio').off('click').on('click', function () {
        var $button = $(this);
        var $form = $button.closest("form");

        if (!$form.valid()) {
            return;
        }

        $button.button('loading');

        var $pdf = $('#pdf');
        $pdf.html('<img src="/Content/images/ajax-loader.gif" style="width: 30px; height: 30px" />');

        $form.ajaxSubmit({
            dataType: 'json',
            cache: false,
            success: function (data) {
                $button.button('reset');
                if (data.Error) {
                    $pdf.empty();
                    alert(data.Error);
                    return;
                }

                var success = new PDFObject({ url: data.Url }).embed("pdf");
                if (!success) {
                    $pdf.html('Não foi possível mostrar o relatório na tela, <a href="' + data.Url + '">clique aqui</a> para baixá-lo.');
                }

                // Se existir um grid, esconder
                $("#Grid").hide();
                
                $(".collapse").on('hidden.bs.collapse', function () {
                    var oldHeight = $pdf.height();
                    var newHeight = $(window).height() - $pdf.offset().top;
                    
                    if (newHeight > oldHeight) {
                        $pdf.height(newHeight);
                    }
                }).collapse('hide');
            },
            error: function (jqXhr, textStatus, errorThrown) {
                $button.button('reset');
                $pdf.empty();
                alert('Ocorreu um erro desconhecido ao gerar o relatório.');
            }
        });
    });

    // Mostra um 'loaging' no meio da tela ao esperar por um carregamento ajax (após 300 milisegundo).
    var timeout = null;
    $(document).on({
        ajaxStart: function () {
            // Não mostrar quando estiver esperando por um relatório
            // pois já mostra um loading por default
            if ($('#pdf').exists())
                return;

            timeout = setTimeout(function () { $('body').addClass('loading'); }, 300);
            document.body.style.cursor = 'wait';
        },
        ajaxStop: function () {
            clearTimeout(timeout);
            $('body').removeClass('loading');
            document.body.style.cursor = 'default';
        }
    });
    
    // Envia o botão Ativar/Inativar como POST
    $(document).on('click', '.btn-editar-situacao', function () {

        $('<form action="' + $(this).prop('href') + '" method="POST"></form>').appendTo('body').submit();
        
        return false;
    });

    $(".naoeditavel :input").each(function () {
        var $this = $(this);
        $this.after("<input type='hidden' name='" + $this.attr('name') + "' value='" + $this.val() + "' />");
        $this.prop("disabled", true);
    });

    //recarrega os dados da grid ajax na Index
    $('#pesquisar-grid-ajax').on('click', function () {

        limpeMensagensAlerta();

        var $ModoConsulta = $('#ModoConsulta');

        if ($ModoConsulta.val() == null || $ModoConsulta.val() == 'Listar') {

            var grid = $('#Grid').data('kendoGrid');

            grid.dataSource.read();
            grid.dataSource.page(1);
            grid.refresh();
            

            return false;
        };
    });

    $('input[name=btn-ModoConsulta]').parent().on('click', function () {

        var $ModoConsulta = $('#ModoConsulta');

        if ($(this).find("input").val() == 'impressao') {
            $ModoConsulta.val('Imprimir');

            $('#row-imprimir').show();
            $('#pesquisar-relatorio').show();
            $('#pesquisar-grid').hide();
            $('#pesquisar-grid-ajax').hide();

        } else {
            $ModoConsulta.val('Listar');
            $("#pdf").html("");
            $("#pdf").attr("style", "");
            $("#Grid").show();
            $('#row-imprimir').hide();
            $('#pesquisar-relatorio').hide();
            $('#pesquisar-grid').show();
            $('#pesquisar-grid-ajax').show();
        }
    });

});

// Permite apenas número
$(document).on('keydown', 'input[type="text"].numeric-only', function (e) {
    var key = e.charCode || e.keyCode || 0;
    // allow backspace, tab, delete, arrows, numbers and keypad numbers ONLY
    // home, end, period, and numpad decimal
    return (key == 8 // backspace
         || key == 9 // tab
         || key == 27 // esc
         || key == 13 // enter
         || key == 46 // del
         || (key == 65 && e.ctrlKey === true) // CTRL + A
         || (key >= 35 && key <= 40) // home, end, left, right
         || (key >= 48 && key <= 57)
         || (key >= 96 && key <= 105));
});

$(document).ready(function () {
    inicializeValidacaoFormulario($('form'));
});

function inicializeValidacaoFormulario(form) {
    $('.input-validation-error').parents('.form-group').addClass('has-error');
    $('.field-validation-error').addClass('text-danger');

    var formData = $.data(form[0]),
        settings = formData.validator.settings,
        oldErrorPlacement = settings.errorPlacement,
        oldSuccess = settings.success,
        oldHighlight = settings.highlight,
        oldUnhighlight = settings.unhighlight;

    settings.errorPlacement = function (label, element) {
        // Call old handler so it can update the HTML
        oldErrorPlacement(label, element);

        // Add Bootstrap classes to newly added elements
        label.parents('.form-group').addClass('has-error');
        label.addClass('text-danger');
    };

    settings.success = function (label) {
        // Remove error class from <div class="form-group">, but don't worry about
        // validation error messages as the plugin is going to remove it anyway
        label.parents('.form-group').removeClass('has-error');

        // Call old handler to do rest of the work
        oldSuccess(label);
    };

    settings.highlight = function (element, errorClass) {
        element = $(element);
        if (element.parent().hasClass("k-widget")) {
            element.parent().addClass('input-validation-error');
        } else {
            element.addClass('input-validation-error');
        }

        oldHighlight(element, errorClass);
    };

    settings.unhighlight = function (element, errorClass) {
        element = $(element);
        if (element.parent().hasClass("k-widget")) {
            element.parent().removeClass('input-validation-error');
        } else {
            element.removeClass('input-validation-error');
            element.parents('.form-group').removeClass('has-error');
            element.parents('.form-group').find(".field-validation-error").html("").text("");
        }
        oldUnhighlight(element, errorClass);
    };
};