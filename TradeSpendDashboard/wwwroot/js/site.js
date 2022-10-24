$(".sidebar-toggle").on("click", function () {
    var sideNav = $("#sidebar");
    var mainContent = $("#main-content");
    sideNav.toggleClass("collapsed");
    var showSideNav = sideNav.hasClass("collapsed");
    if (showSideNav) {
        sideNav.removeClass("fixed-top");
        mainContent.removeClass("sidebar-show");
    } else {
        sideNav.addClass("fixed-top");
        mainContent.addClass("sidebar-show");
    }

    $("#top-navbar").toggleClass("show-sidebar");
})
