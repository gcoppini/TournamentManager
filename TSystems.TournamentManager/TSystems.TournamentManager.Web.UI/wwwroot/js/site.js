// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

    $(document).ready(function () { 
        $("#checkAll").click(function(){
            $('input:checkbox').prop('checked', false);
            
            var must_check = 20;
            // Count checkboxes
            var checkboxes = $("input:checkbox").length;
            console.log('antes');
            // Check random checkboxes until "must_check" limit reached
            while ($("input:checkbox").filter(":checked").length < must_check) {
                // Pick random checkbox
                var random_checkbox = Math.floor(Math.random() * checkboxes);
                // Check it
                $("input:checkbox").eq(random_checkbox).prop("checked", true);
                console.log('depois');
            }
            
        })
    });
