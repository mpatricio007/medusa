function pageLoad()
{
    $(function ()
    {
        $('.date').datepicker({
            showOtherMonths: true,
            changeMonth: true,
            changeYear: true
        });
        $('.date').dateEntry({ spinnerImage: '' });
        $('.cep').mask("99999-999");
        $(".cpf").mask("999.999.999-99");
        $(".telefone").mask("(99)99999999?9");
        $(".cnpj").mask("99.999.999/9999-99");
        $(".valor").maskMoney({showSymbol:false,decimal:",", thousands:"."});
        // create the loading window and set autoOpen to false
	
    });
}


	