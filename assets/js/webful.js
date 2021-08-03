// JavaScript Document
(function($) {
    "use strict";

	//calling foundation js
	$(document).foundation();
	
	//scroll effect
	$(document).ready(function () {

		$("#top").on('click',function () {
			$("html, body").animate({ scrollTop: 0 }, "slow");
			return false;
		});        

		jQuery('#top').on('click',function (event) {
			event.stopPropagation();                
			var idTo = jQuery(this).attr('data-atr');                
			var Position = jQuery('[id="' + idTo + '"]').offset().top;
			jQuery('html, body').animate({ scrollTop: Position }, 'slow');
			return false;
		});
	
	});

	$(window).on('scroll',function() { 
		if($(this).scrollTop() > 1000) { 
			$("#top").fadeIn();
		} else { 
			$("#top").fadeOut();
		}
	});
	
	//TwentyTwenty Plugin Starter.
	$(window).load(function(){
	  $(".twentytwenty-container[data-orientation!='vertical']").twentytwenty();
	});
	
	// Saying Page Loaded
	$(window).on('load',function(){
		$('body').addClass('loaded');
		$('.preloader').html('');
		$('.preloader').css('display', 'none');
	 });

	//calling Brand Crousel
	$('.main-banner').owlCarousel({
		loop:true,
		margin:0,
		autoplay:true,
		responsiveClass:true,
		dots:true,
		responsive:{
			0:{
				items:1,
				nav:true, 			
			},
			600:{
				items:1,
				nav:false, 			
			},
			1000:{
				items:1,
				nav:false, 			
			}
		}
	});

	//Services mouse over action
	$(".service").on("mouseenter", function() { 
		var animationEnd = "webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend";
		$(this).children(".service-box").children(".service-img").children(".service-detail").addClass("animated bounceInUp").on(animationEnd, function() {
			$(this).removeClass("animated bounceInUp");
		});
	});

	//Our Team Mouse Over Actions
	$(".doctor").on("mouseenter", function() { 
		var animationEnd = "webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend";
		$(this).children(".doctor-box").children(".doctor-img").children(".doctor-detail").addClass("animated fadeInLeft").on(animationEnd, function() {
			$(this).removeClass("animated fadeInLeft");
		});
	});



var owl = $('.testimonials');
owl.owlCarousel({
    items:1,
    loop:true,
    margin:0,
    autoplay:true,
    autoplayTimeout:4000,
	nav:true,
	navText:["<i class='fa fa-angle-left'></i>","<i class='fa fa-angle-right'></i>"],
    autoplayHoverPause:true
});
$('.play').on('click',function(){
    owl.trigger('autoplay.play.owl',[1000]);
});
$('.stop').on('click',function(){
    owl.trigger('autoplay.stop.owl');
});

$(document).ready(function(){
    $('.customer-logos').slick({
        slidesToShow: 6,
        slidesToScroll: 1,
        autoplay: true,
        autoplaySpeed: 1500,
        arrows: false,
        dots: false,
        pauseOnHover: false,
        responsive: [{
            breakpoint: 768,
            settings: {
                slidesToShow: 4
            }
        }, {
            breakpoint: 520,
            settings: {
                slidesToShow: 3
            }
        }]
    });
});

//calling Brand Crousel
$('.brand-carousel').owlCarousel({
	loop:true,
	margin:10,
	responsiveClass:true,
	responsive:{
		0:{
			items:1,
			nav:true,
			navText:["<i class='fa fa-angle-left'></i>","<i class='fa fa-angle-right'></i>"]
		},
		600:{
			items:3,
			nav:false,
			navText:["<i class='fa fa-angle-left'></i>","<i class='fa fa-angle-right'></i>"]
		},
		1000:{
			items:5,
			nav:true,
			navText:["<i class='fa fa-angle-left'></i>","<i class='fa fa-angle-right'></i>"],
			loop:true
		}
	}
});

})(jQuery); //jQuery main function ends strict Mode on 

(function(i,s,o,g,r,a,m){i['GoogleAnalyticsObject']=r;i[r]=i[r]||function(){
  (i[r].q=i[r].q||[]).push(arguments)},i[r].l=1*new Date();a=s.createElement(o),
  m=s.getElementsByTagName(o)[0];a.async=1;a.src=g;m.parentNode.insertBefore(a,m)
  })(window,document,'script','https://www.google-analytics.com/analytics.js','ga');

  ga('create', 'UA-62711679-1', 'auto');
  ga('send', 'pageview');
  
  
  var isNS = (navigator.appName == "Netscape") ? 1 : 0;

if(navigator.appName == "Netscape") document.captureEvents(Event.MOUSEDOWN||Event.MOUSEUP);

function mischandler(){
return false;
}

function mousehandler(e){
var myevent = (isNS) ? e : event;
var eventbutton = (isNS) ? myevent.which : myevent.button;
if((eventbutton==2)||(eventbutton==3)) return false;
}
/*document.oncontextmenu = mischandler;
document.onmousedown = mousehandler;
document.onmouseup = mousehandler;*/


/*document.onkeydown = function(e) {
        if (e.ctrlKey && 
            (e.keyCode === 67 || 
             e.keyCode === 86 || 
             e.keyCode === 85 || 
             e.keyCode === 117)) {
            alert('not allowed');
            return false;
        } else {
            return true;
        }
};*/



