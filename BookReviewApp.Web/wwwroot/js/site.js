// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Modern JavaScript enhancements for BookReviewApp
(function () {
    'use strict';

    // Toast notification system
    window.showToast = function (message, type = 'success', duration = 4000) {
        const toastOptions = {
            text: message,
            duration: duration,
            gravity: "top",
            position: "right",
            style: {
                background: type === 'success' ? 'linear-gradient(135deg, #56ab2f 0%, #a8e6cf 100%)' :
                          type === 'error' ? 'linear-gradient(135deg, #ff416c 0%, #ff4b2b 100%)' :
                          'linear-gradient(135deg, #667eea 0%, #764ba2 100%)',
                color: '#fff',
                borderRadius: '12px',
                boxShadow: '0 8px 32px rgba(0,0,0,0.2)',
                fontSize: '14px',
                fontWeight: '500'
            }
        };
        
        if (typeof Toastify !== 'undefined') {
            Toastify(toastOptions).showToast();
        } else {
            // Fallback to Bootstrap alerts
            const alertClass = type === 'success' ? 'alert-success' : 
                              type === 'error' ? 'alert-danger' : 'alert-info';
            const alertHtml = `
                <div class="alert ${alertClass} alert-dismissible fade show" role="alert">
                    <i class="bi bi-${type === 'success' ? 'check-circle-fill' : 
                                     type === 'error' ? 'exclamation-triangle-fill' : 'info-circle-fill'} me-2"></i>
                    ${message}
                    <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                </div>
            `;
            const container = document.querySelector('main.container');
            if (container) {
                container.insertAdjacentHTML('afterbegin', alertHtml);
            }
        }
    };

    // Loading state management
    window.showLoading = function (element) {
        if (element) {
            element.classList.add('loading');
            element.disabled = true;
        }
    };

    window.hideLoading = function (element) {
        if (element) {
            element.classList.remove('loading');
            element.disabled = false;
        }
    };

    // Enhanced form validation
    function enhanceFormValidation() {
        const forms = document.querySelectorAll('form');
        forms.forEach(form => {
            form.addEventListener('submit', function (e) {
                const submitBtn = form.querySelector('button[type="submit"]');
                if (submitBtn) {
                    showLoading(submitBtn);
                }
            });
        });
    }

    // Smooth scrolling for anchor links
    function initSmoothScrolling() {
        document.querySelectorAll('a[href^="#"]').forEach(anchor => {
            anchor.addEventListener('click', function (e) {
                e.preventDefault();
                const target = document.querySelector(this.getAttribute('href'));
                if (target) {
                    target.scrollIntoView({
                        behavior: 'smooth',
                        block: 'start'
                    });
                }
            });
        });
    }

    // Enhanced card interactions
    function enhanceCardInteractions() {
        const cards = document.querySelectorAll('.book-card, .author-card, .review-card');
        cards.forEach(card => {
            card.addEventListener('mouseenter', function () {
                this.style.transform = 'translateY(-8px) scale(1.02)';
            });
            
            card.addEventListener('mouseleave', function () {
                this.style.transform = 'translateY(0) scale(1)';
            });
        });
    }

    // Auto-hide alerts after 5 seconds
    function initAutoHideAlerts() {
        const alerts = document.querySelectorAll('.alert');
        alerts.forEach(alert => {
            setTimeout(() => {
                const bsAlert = new bootstrap.Alert(alert);
                bsAlert.close();
            }, 5000);
        });
    }

    // Enhanced button interactions
    function enhanceButtonInteractions() {
        const buttons = document.querySelectorAll('.btn');
        buttons.forEach(button => {
            button.addEventListener('mouseenter', function () {
                this.style.transform = 'translateY(-2px)';
            });
            
            button.addEventListener('mouseleave', function () {
                this.style.transform = 'translateY(0)';
            });
        });
    }

    // Lazy loading for images
    function initLazyLoading() {
        const images = document.querySelectorAll('img[data-src]');
        const imageObserver = new IntersectionObserver((entries, observer) => {
            entries.forEach(entry => {
                if (entry.isIntersecting) {
                    const img = entry.target;
                    img.src = img.dataset.src;
                    img.classList.remove('lazy');
                    observer.unobserve(img);
                }
            });
        });

        images.forEach(img => imageObserver.observe(img));
    }

    // Enhanced navigation active states
    function initNavigationActiveStates() {
        const currentPath = window.location.pathname;
        const navLinks = document.querySelectorAll('.navbar-nav .nav-link');
        
        navLinks.forEach(link => {
            const href = link.getAttribute('href');
            if (href && currentPath.includes(href.split('/')[1])) {
                link.classList.add('active');
            }
        });
    }

    // Animate statistics numbers on scroll
    function animateStats() {
        const stats = document.querySelectorAll('.stat-number[data-animate]');
        stats.forEach(stat => {
            const endValue = parseFloat(stat.getAttribute('data-animate'));
            let startValue = 0;
            let duration = 1200;
            let startTime = null;
            function animateStep(timestamp) {
                if (!startTime) startTime = timestamp;
                const progress = Math.min((timestamp - startTime) / duration, 1);
                const value = Math.floor(progress * (endValue - startValue) + startValue);
                stat.textContent = endValue % 1 === 0 ? value : value.toFixed(1);
                if (progress < 1) {
                    requestAnimationFrame(animateStep);
                } else {
                    stat.textContent = endValue;
                }
            }
            // Only animate if in viewport
            function onScroll() {
                const rect = stat.getBoundingClientRect();
                if (rect.top < window.innerHeight && rect.bottom > 0) {
                    requestAnimationFrame(animateStep);
                    window.removeEventListener('scroll', onScroll);
                }
            }
            window.addEventListener('scroll', onScroll);
            onScroll();
        });
    }

    // Initialize testimonial carousel
    function initTestimonialCarousel() {
        const carousel = document.getElementById('testimonialCarousel');
        if (carousel && typeof bootstrap !== 'undefined') {
            new bootstrap.Carousel(carousel, { interval: 6000, ride: 'carousel' });
        }
    }

    // Initialize all enhancements when DOM is ready
    document.addEventListener('DOMContentLoaded', function () {
        enhanceFormValidation();
        initSmoothScrolling();
        enhanceCardInteractions();
        initAutoHideAlerts();
        enhanceButtonInteractions();
        initLazyLoading();
        initNavigationActiveStates();
        animateStats();
        initTestimonialCarousel();

        // Show success/error messages as toasts if they exist
        const successMessage = document.querySelector('.alert-success');
        const errorMessage = document.querySelector('.alert-danger');
        
        if (successMessage) {
            const message = successMessage.textContent.trim();
            showToast(message, 'success');
        }
        
        if (errorMessage) {
            const message = errorMessage.textContent.trim();
            showToast(message, 'error');
        }
    });

    // Global error handler
    window.addEventListener('error', function (e) {
        console.error('Global error:', e.error);
        showToast('An unexpected error occurred. Please try again.', 'error');
    });

    // Handle AJAX errors
    if (typeof $ !== 'undefined') {
        $(document).ajaxError(function (event, xhr, settings, error) {
            showToast('An error occurred while processing your request.', 'error');
        });
    }

})();
