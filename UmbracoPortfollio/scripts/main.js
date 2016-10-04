jQuery(document).ready(function ($) {
        var timelineBlocks = $('.timeline-block'),
            offset = 0.8;

        //hide timeline blocks which are outside the viewport
        hideBlocks(timelineBlocks, offset);

        //on scolling, show/animate timeline blocks when enter the viewport
        $(window).on('scroll', function () {
            (!window.requestAnimationFrame)
                ? setTimeout(function () { showBlocks(timelineBlocks, offset); }, 100)
                : window.requestAnimationFrame(function () { showBlocks(timelineBlocks, offset); });
        });

        function hideBlocks(blocks, offset) {
            blocks.each(function () {
                ($(this).offset().top > $(window).scrollTop() + $(window).height() * offset) && $(this).find('.timeline-img, .timeline-content').addClass('is-hidden');
            });
        }

        function showBlocks(blocks, offset) {
            blocks.each(function () {
                ($(this).offset().top <= $(window).scrollTop() + $(window).height() * offset && $(this).find('.timeline-img').hasClass('is-hidden')) && $(this).find('.timeline-img, .timeline-content').removeClass('is-hidden').addClass('bounce-in');
            });
        }
    });

    $(document).ready(function () {
        /*Link directly to an open modal window with, a quick and dirty way. TODO: adopt angular sooner or later*/
        var target = document.location.hash.replace("#", "").toLowerCase();
        if (target.length) {
            if (target == "books") {
                $('#Books').modal('show');
            }
            else if (target == "talks") {
                $('#Talks').modal('show');
            }
            else if (target == "places") {
                $('#Places').modal('show');
            }
            else if (target == "movie") {
                $('#Movie').modal('show');
            }
            else if (target == "work") {
                $('#Work').modal('show');
            }
            else if (target == "sideprojects") {
                $('#SideProjects').modal('show');
            }
            else if (target == "skills") {
                $('#Skills').modal('show');
            }
            else if (target == "summer") {
                $('#summer').modal('show');
            }

        }
        /* init tooltip and popovers*/
        $('[data-toggle="tooltip"]').tooltip({

        });

        $('[data-toggle="popover"]').popover({
            trigger: 'hover'
        });
        $("[rel='tooltip']").tooltip();
    });   

    $(function () {
        $(window).scroll(function () {
            if ($(this).scrollTop() != 0) {
                $('#toTop').fadeIn();
            } else {
                $('#toTop').fadeOut();
            }
        });

        $('#toTop').click(function () {
            $('body,html').animate({ scrollTop: 0 }, 800);
        });

    });

    $(document).ready(function () {
        $(".owl-carousel").owlCarousel({
            loop: true,
            responsiveClass: true,
            responsive: {
                0: {
                    items: 1,
                    nav: true,
                    loop: true
                },
                600: {
                    items: 2,
                    nav: true,
                    loop: true
                },
                1000: {
                    items: 4,
                    nav: true,
                    loop: true
                }
            },
            nav: true,
            navText: [
              "<i class='glyphicon glyphicon-chevron-left icon-white'></i>",
              "<i class='glyphicon glyphicon-chevron-right icon-white'></i>"
            ],
        });
    });
