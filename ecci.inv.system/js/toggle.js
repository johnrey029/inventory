$(document).ready(function () {
    //$(".x-icon").click(function () {
    //    $(".x-icon").toggleClass("active");
    //});
    $(".x-icon").click(function () {
        $(".sidebar").toggleClass("active");
    });

    $(".submenu").click(function () {
        $(".submenu").toggleClass("tap");
    });
    $(".submenu1").click(function () {
        $(".submenu1").toggleClass("tap");
    });
});

function closesidemenu() {
    document.getElementById('mainpage').style.marginLeft = '0px';
    document.getElementById('mainpage').style.maxWidth = '100%';
    document.getElementById('hambuger').style.left = '0px';
    document.getElementById('xclose').style.left = '-200px';

}
function opensidemenu() {

    document.getElementById('mainpage').style.marginLeft = '250px';
    document.getElementById('mainpage').style.maxWidth = '83.6%';
    document.getElementById('hambuger').style.left = '-200px';
    document.getElementById('xclose').style.left = '0px';
    $(".sidebar").toggleClass("active");
}