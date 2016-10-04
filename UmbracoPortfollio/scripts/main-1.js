(function( $ ) {
	"use strict";

	var divsToShow = $("#home, #studio, #projects, #contact, .footer");

	$(window).load(function(){
		$(".loader").css("display","none");
		$("html").css("overflow","auto");
		divsToShow.css("opacity","1");
	});

//	$('input').iCheck();

	// Flexslider
	$(window).on('load', function() {
		var viewportWidth = window.innerWidth;

		if(viewportWidth < 1024){
			$('.work').flexslider({
				controlNav: false,
				directionNav: true,
				animationLoop: true,
				useCSS: false,
				animation: "slide",
				smoothHeight: true,
				pauseOnHover: false,
				slideshowSpeed: 500,
				easing: "easeOutCubic",
				animationSpeed: 800,
				direction: "horizontal",
				reverse: false,
				pauseOnAction: true,
				slideshow: false,
			});

			$('.work--second').flexslider({
				controlNav: false,
				directionNav: false,
				animationLoop: true,
				useCSS: false,
				animation: "slide",
				smoothHeight: true,
				pauseOnHover: false,
				slideshowSpeed: 500,
				easing: "easeOutCubic",
				animationSpeed: 800,
				touch: false,
				pauseOnAction: false,
				direction: "horizontal",
				reverse: false,
				slideshow: false,
				sync: ".work"
			});

			$('.work a.flex-prev').click(function(){
				$('.work--second').flexslider("previous");
			});

			$('.work a.flex-next').click(function(){
				$('.work--second').flexslider("next");
			});

			$(function(){
				$(".work").swipe({
					swipeRight:function(event, direction, distance, duration, fingerCount, fingerData){
						$('.work--second').flexslider("previous");
					},
					swipeLeft:function(event, direction, distance, duration, fingerCount, fingerData){
						$('.work--second').flexslider("next");
					}
				});
			});
		}else{
			$('.work').flexslider({
				controlNav: false,
				directionNav: true,
				animationLoop: true,
				useCSS: false,
				animation: "slide",
				smoothHeight: true,
				pauseOnHover: false,
				slideshowSpeed: 8000,
				easing: "easeOutCubic",
				pauseOnAction: true,
				animationSpeed: 800,
				direction: "vertical",
				reverse: false,
				slideshow: false,
			});

			$('.work--second').flexslider({
				controlNav: false,
				directionNav: false,
				animationLoop: true,
				useCSS: false,
				animation: "slide",
				smoothHeight: true,
				pauseOnHover: false,
				slideshowSpeed: 8000,
				touch: false,
				easing: "easeOutCubic",
				animationSpeed: 800,
				direction: "vertical",
				reverse: false,
				slideshow: false,
				sync: ".work"
			});

			$('.work a.flex-prev').click(function(){
				$('.work--second').flexslider("previous");
			});

			$('.work a.flex-next').click(function(){
				$('.work--second').flexslider("next");
			});

			$(function(){
				$(".work").swipe({
					swipeDown:function(event, direction, distance, duration, fingerCount, fingerData){
						$('.work--second').flexslider("previous");
					},
					swipeUp:function(event, direction, distance, duration, fingerCount, fingerData){
						$('.work--second').flexslider("next");
					}
				});
			});
		}
	});

	// Scroll
	$('.navigation a:not([target=_blank]), .header__button, .header__image__logo').on('click', function(e) {

		e.preventDefault();

		var this_el = $(this),
			target_el = this_el.attr('href');

		if(viewportWidth >= 1024){
			$('html, body').animate({
				scrollTop: $(target_el).offset().top - 80
			}, 800, "easeOutCubic");
			return false;
		}else{
			$('html, body').animate({
				scrollTop: $(target_el).offset().top
			}, 800, "easeOutCubic");
			return false;
		}


	});

	// Mobile menu dropdown
	var nav = $(".navigation"),
		navButton = $(".navigation__button"),
		navContainer = $(".navigation__container");

	navButton.click(function(){
		navContainer.slideToggle();
	});

	// Header image
	var headerImage = $(".header__image"),
		viewportWidth = window.innerWidth,
		viewportHeight = $(window).height();

	if(viewportWidth >= 1024){
		headerImage.outerHeight(viewportHeight);
	}

	$(window).resize(function(){
		var viewportWidth = window.innerWidth,
			viewportHeight = $(window).height();

		if(viewportWidth >= 1024){
			headerImage.outerHeight(viewportHeight);
		}else if(viewportWidth < 1024){
			headerImage.css("height","auto");
		}
	});

	// Midnight

	$(document).ready(function(){
		$(window).scroll(function(){
			var scrollTop = $(window).scrollTop(),
				elemPos = $(".header__image__logo").offset(),
				elemPosStudio = $("#studio").offset(),
				vw = window.innerWidth;

			if(scrollTop < elemPos.top && vw >= 1024){
				$(".header__image__logo").attr("data-midnight", "mdn-1");
				$(".fixed").midnight();
				$(".midnightHeader.default .logo").css("display","none");
			}else{
				$(".header__image__logo").removeAttr("data-midnight");
				$(".midnightHeader.default .logo").css("display","block");
			}
		});
		$(window).resize(function(){
			var scrollTop = $(window).scrollTop(),
				elemPos = $(".header__image__logo").offset(),
				vw = window.innerWidth;

			if(scrollTop < elemPos.top && vw >= 1024){
				$(".header__image__logo").attr("data-midnight", "mdn-1");
				$(".fixed").midnight();
				$(".midnightHeader.default .logo").css("display","none");
			}else{
				$(".header__image__logo").removeAttr("data-midnight");
				$(".midnightHeader.default .logo").css("display","block");
			}

			if(vw < 1024){
				$(".navigation").css({position: "static"}).removeAttr("style");
				$(".fixed").css("display","none");
				$(".navigation--mobile").css("display","block");
			}else{
				$(".navigation").css({position: "fixed"});
				$(".fixed").css("display","block");
				$(".navigation--mobile").css("display","none");
			}

			if(vw < 600){
				$(".navigation__container").css("display","none");
			}else{
				$(".navigation__container").css("display","block");
			}
		});
	});

	// Contact info list spacing

	$(window).load(function(){
		var listSpacing = function(){
			var formWidth = $('.contact__form').width();

			var listWidth = $('.contact__info li').map(function(){
				return $(this).width();
			}).get();

			var result = 0;

			for (var i = 0; i < listWidth.length; i++) {
				result += listWidth[i];
			}

			var spacing = ((formWidth - result) / 3) - 5;

			$('.contact__info li').css("marginRight", spacing);
		},
			elseSpacing = function(){
				$('.contact__info li').css("marginRight", 81);
				$('.contact__info li').last().css("marginRight", 0);
			}

		if(viewportWidth < 1366){
			listSpacing();
		}else if(viewportWidth >= 1366){
			elseSpacing();
		}

		$(window).resize(function(){
			var viewportWidth = window.innerWidth;

			if(viewportWidth < 1366){
				listSpacing();
			}else if(viewportWidth >= 1366){
				elseSpacing();
			}
		});
	});

	//Nav color change

	$(window).scroll(function(){
		var viewportWidth = window.innerWidth,
			scrollTop = $(window).scrollTop(),
			studioPos = $("#studio").offset(),
			projectsPos = $(".work__holder").offset(),
			contactPos = $("#contact").offset();

		if(viewportWidth >= 1024 && scrollTop >= studioPos.top - 80){
		    $(".navigation").css("background-color", "#583A52");
			$(".navigation li a").css("color","#fff");
		}else{
			$(".navigation").css("background-color","#fff");
			$(".navigation li a").css("color","#556671");
		}
	});

	$(window).load(function(){
		var viewportWidth = window.innerWidth,
			scrollTop = $(window).scrollTop(),
			studioPos = $("#studio").offset(),
			projectsPos = $(".work__holder").offset(),
			contactPos = $("#contact").offset();

		if(viewportWidth >= 1024 && scrollTop >= studioPos.top - 80){
		    $(".navigation").css("background-color", "#583A52");
			$(".navigation li a").css("color","#fff");			
		}else{
			$(".navigation").css("background-color","#fff");
			$(".navigation li a").css("color","#556671");			
		}
	});

	//Form

	$(document).ready(function(){
		$.validate();
	});

	$("#check").click(function(){
		$(this).toggleClass("valid");
	});

	$('body').on('click', '.button--submit', function() {

		$(".form__submit__success").remove();
		$(".form__submit__error").remove();

		var cf = $('#form');

		$.ajax({
			type : 'POST',
			url : cf.attr('action'),
			// dataType : 'json',
			data: cf.serialize(),
			success : function(data) {

				console.log(data);
				if(data === "200" || data === "204"){
					/*
					$("#form")
						.css("display","none");
					*/
					$(".contact__form")
						.prepend("<p class='form__submit__success'>Thank you for contacting us. We will get in touch with you shortly.</p>");
					$("input#name, input#email, input#check, textarea").val('');
					$("#check").prop('checked', false).removeClass("valid");
				}else{
					$(".contact__form")
						.prepend("<p class='form__submit__error'>Please fill in all of the required fields.</p>");
					$("input#name, input#email, input#check, textarea").val('');
					$("#check").prop('checked', false).removeClass("valid");
				}



			},
			error : function(XMLHttpRequest, textStatus, errorThrown) {

				alert('ajax failed');

			}
		});

		return false;

	})

}(jQuery));