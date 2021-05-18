
$("[name=CompletionSelectionOption]").change(e => {

    checkInteractiveOptions();

})


$("[name=IncludeOnlyVacationEntries]").change(e => {

    checkInteractiveOptions();
});


$("[name=AzureTableOptionSelected]").change(e => {

    checkInteractiveOptions();
});


function checkInteractiveOptions() {

 
    let url = document.location.protocol + "//" + document.location.host;
    let url_Suffix = [];

    const CompletionSelectionOption_selected = document.querySelectorAll("[name=CompletionSelectionOption]");

    CompletionSelectionOption_selected.forEach(obj1 => {
       
        if (obj1.checked) {
            url_Suffix.push("CompletionSelectionOption=" + obj1.value);
        }
    });


    const AzureTableOptionSelected_selected = document.querySelectorAll("[name=AzureTableOptionSelected]");

    AzureTableOptionSelected_selected.forEach(obj1 => {
         
        if (obj1.checked) {
            url_Suffix.push("AzureTableOptionSelected=" + obj1.value);
        }
    });



    const IncludeOnlyVacationEntries_selected = document.querySelectorAll("[name=IncludeOnlyVacationEntries]");


    if (IncludeOnlyVacationEntries_selected.length > 0 && IncludeOnlyVacationEntries_selected[0].checked) {
        url_Suffix.push("IncludeOnlyVacationEntries=true");
    }

    var url_suffix_final = '';

    if (url_Suffix.length > 0) {
        url_suffix_final = '?' + url_Suffix.join('&')
    }


    document.location = document.location.protocol + "//" + document.location.host + url_suffix_final;

}



function checkQuery() {

    const search = document.location.search.replace('?', '');
    const queryOptions = search.split('&');

    const url_Suffix = [];
    let haveBadOptions = false;

    queryOptions.forEach(e => {

        const keyValue = e.split('=');

        
        if (keyValue[0].toLowerCase() === "AzureTableOptionSelected".toLowerCase()) {

            if (validAzureTables.find(e => e == keyValue[1])) {
                url_Suffix.push(keyValue[0] + '=' + keyValue[1]);

            } else {
                haveBadOptions = true;
            }
        }


        if (keyValue[0].toLowerCase() === "CompletionSelectionOption".toLowerCase()) {

            if (validEntries.find(e => e == keyValue[1])) {
                url_Suffix.push(keyValue[0] + '=' + keyValue[1]);

            } else {
                haveBadOptions = true;
            }
        }

        if (keyValue[0].toLowerCase() === "IncludeOnlyVacationEntries".toLowerCase()) {

            if (keyValue[1].toLowerCase() != 'true') {
                haveBadOptions = true;
            } else {
                url_Suffix.push(keyValue[0] + '=' + keyValue[1]);
            }
        }


    })


    if (haveBadOptions) {

        var url_suffix_final = '';

        if (url_Suffix.length > 0) {
            url_suffix_final = '?' + url_Suffix.join('&')
        }
       

        document.location = document.location.protocol + "//" + document.location.host + url_suffix_final;
    }


}


checkQuery();