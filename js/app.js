$(document).ready(function(){
// Menubar
var $togglefunction = function(){
 	$('.menubar').toggleClass('active');
	$('.wrapper').toggleClass('active');
	$('.buttonm').toggleClass('active');
};
$('#button').on('click', function(){
	$togglefunction();
});
$('.menubar #menupage').on('click', function(){
	$togglefunction();
});
// Menubar fix (scroll)
$(window).on('scroll', function(){
  $('header').toggleClass('scrollfix', scrollY > 0 );
});

});