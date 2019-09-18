(function() {

    /*var ele = $("#username");
    ele.text("User Name");

    var main = $("#main");
    main.on("mouseenter",
        function() {
            main.style.backgroundColor = "#888";
        });

    main.on("mouseleave",
        function() {
            main.style.backgroundColor = "";
        });

    var menuItems = $("ul.menu li a");
    menuItems.on("click",
        function() {
            var me = $(this);
            alert(me.text());
        });*/

    var $sidebarAndWrapper = $("#sidebar,#wrapper");
    var $icon = $("#sidebarToggle i.fa")

    $("#sidebarToggle").on("click",
        function() {
            $sidebarAndWrapper.toggleClass("hide-sidebar");
            if ($sidebarAndWrapper.hasClass("hide-sidebar")) {
                $icon.removeClass("fa-chevron-left")
                $icon.addClass("fa-chevron-right")
            } else {
                $icon.removeClass("fa-chevron-right")
                $icon.addClass("fa-chevron-left")
            }
        });
})();

