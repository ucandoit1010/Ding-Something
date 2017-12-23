// Write your JavaScript code.

$(document).ready(function(){

    $('#imgFile').change(function(){
        var extion = $('#imgFile').val().split('.').pop().toLowerCase();
        if($.inArray(extion, ['png','jpg']) == -1) {
            $('#imgFile').val('');
            alert('限制 PNG , JPG');
        }


    });

    if(window.location.pathname.indexOf('Checkout') > 0){

        $(document).ready(loadCheckoutButton);
    }
    

    var box = $('.portfolio-item');

    $('.btn-filter').click(function(){
        var filter = $(this).attr('data-filter');
        switch(filter){
            case 'all':
                box.removeClass('is-animated');
                box.each(function(i) {
                    $(this).addClass('is-animated').delay((i++) * 200).fadeIn();
                });
                break;
            default:
                box.removeClass('is-animated').fadeOut().finish().promise().done(function() {
                    
                    box.filter('[data-category = "' + parseInt(filter) + '"]').each(function(i) {
                        $(this).addClass('is-animated').delay((i++) * 200).fadeIn();
                    });
                });

                break;
        }
    });

    $('select#selCatalog').change(function(){
        loadProduct();
    });

});

function isNumber(val){
    if(val === '0'){
        return false;
    }
    var reg = /^\d+$/;
    val.replace('\'','').replace('"','');
    return reg.test(val);
}

function ajax(url , param , successfunc){
    $.ajax({url: url, data : param, method:'POST',async:false}).
        done(function(resp) {
        successfunc(resp);
    });
}

function loadSelectAjax(url , selectId ,data, selectedVal){

    selectId.find('option').remove();
    $.get(url ,data, function(resp) {
        var html = '<option value="';
        $(resp).each(function(a,b){
            // if(selectedVal && b.value === selectedVal){
            //     html = '<option selected="selected" value="';
            // }
            selectId.append(html + b.value + '">' + b.text + '</option>');
        });
        if(selectedVal){
            selectId.val(selectedVal);
        }
    }).fail(function() {
        alert("error");
    });
}