$(()=>{
    selectStackHolder();
    $('#StackholderType').on('change', selectStackHolder);

    
})
function selectStackHolder() {
    debugger
    var valueSelected = this.value;
    switch (valueSelected) {
        case "Farmer":
            $('.form-group').show();
            $('.form-group:not(".farmer-control"):not(".common-control"):not(".Stackholder-list")').hide();
            //$('.form-group:not(".common-control")').hide();
            break;
        case "Supplier":
            $('.form-group').show();
            $('.form-group:not(".supplier-control"):not(".common-control"):not(".Stackholder-list")').hide();
            //$('.form-group:not(".common-control")').hide();
            break;
        case "Producer":
            $('.form-group').show();
            $('.form-group:not(".producer-control"):not(".common-control"):not(".Stackholder-list")').hide();
            //$('.form-group:not(".common-control")').hide();
            break;
        case "Logistics":
            $('.form-group').show();
            $('.form-group:not(".logistics-control"):not(".Stackholder-list")').hide();
            //$('.form-group:not(".common-control")').hide();
            break;
        default:
            $('.form-group').show();
            $('.form-group:not(".farmer-control"):not(".common-control"):not(".Stackholder-list")').hide();
            break;
    }
}