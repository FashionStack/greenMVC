window.addEventListener('DOMContentLoaded', event => {

    const sidebarToggle = document.body.querySelector('#sidebarToggle');
    if (sidebarToggle) {

        sidebarToggle.addEventListener('click', event => {
            event.preventDefault();
            document.body.classList.toggle('sb-sidenav-toggled');
            localStorage.setItem('sb|sidebar-toggle', document.body.classList.contains('sb-sidenav-toggled'));
        });
    }

});

function adjustImage() {
    window.onload = function () {        
        $("#product-image").hide();
        $("#tagImage").show();
    }
}

function checkboxFix() {
    var elem = $("input[type=checkbox]");
    if (elem.val() == "true") {
        $("input[type=checkbox]").prop("checked", true);
    }
    else {
        $("input[type=checkbox]").prop("checked", false);
    }

    elem.on("change", function () {
        var $this = $(this);
        if ($this.is(":checked")) {
            $this.val('true');
        }
        else {
            $this.val('false');
        }
    });
}