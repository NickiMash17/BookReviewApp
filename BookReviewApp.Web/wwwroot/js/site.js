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
            // Skip authentication forms to prevent interference
            if (form.action && (form.action.includes('/Account/Login') || form.action.includes('/Account/Register'))) {
                return;
            }
            
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

    // Enhanced Navigation Functionality
    document.addEventListener('DOMContentLoaded', function() {
        
        // Search functionality
        const searchInput = document.querySelector('.search-input');
        const searchBtn = document.querySelector('.search-btn');
        
        if (searchInput && searchBtn) {
            // Search button click
            searchBtn.addEventListener('click', function() {
                performSearch();
            });
            
            // Enter key press in search input
            searchInput.addEventListener('keypress', function(e) {
                if (e.key === 'Enter') {
                    performSearch();
                }
            });
            
            // Search input focus effects
            searchInput.addEventListener('focus', function() {
                this.parentElement.classList.add('search-focused');
            });
            
            searchInput.addEventListener('blur', function() {
                this.parentElement.classList.remove('search-focused');
            });
        }
        
        // Perform search function
        function performSearch() {
            const query = searchInput.value.trim();
            if (query) {
                // Show loading state
                searchBtn.innerHTML = '<i class="bi bi-arrow-clockwise spin"></i>';
                searchBtn.disabled = true;
                
                // Simulate search (replace with actual search implementation)
                setTimeout(() => {
                    // Redirect to search results or show results
                    window.location.href = `/Books?search=${encodeURIComponent(query)}`;
                    
                    // Reset search button
                    searchBtn.innerHTML = '<i class="bi bi-search"></i>';
                    searchBtn.disabled = false;
                }, 500);
            }
        }
        
        // Enhanced navbar scroll effects
        const navbar = document.querySelector('.navbar');
        let lastScrollTop = 0;
        
        window.addEventListener('scroll', function() {
            const scrollTop = window.pageYOffset || document.documentElement.scrollTop;
            
            if (scrollTop > 100) {
                navbar.classList.add('navbar-scrolled');
            } else {
                navbar.classList.remove('navbar-scrolled');
            }
            
            // Hide/show navbar on scroll (optional)
            if (scrollTop > lastScrollTop && scrollTop > 200) {
                navbar.style.transform = 'translateY(-100%)';
            } else {
                navbar.style.transform = 'translateY(0)';
            }
            
            lastScrollTop = scrollTop;
        });
        
        // Smooth navigation link animations
        const navLinks = document.querySelectorAll('.nav-link-enhanced');
        navLinks.forEach(link => {
            link.addEventListener('mouseenter', function() {
                this.style.transform = 'translateY(-2px)';
            });
            
            link.addEventListener('mouseleave', function() {
                this.style.transform = 'translateY(0)';
            });
        });
        
        // Enhanced dropdown animations
        const dropdowns = document.querySelectorAll('.dropdown');
        dropdowns.forEach(dropdown => {
            const menu = dropdown.querySelector('.dropdown-menu');
            
            dropdown.addEventListener('show.bs.dropdown', function() {
                menu.style.opacity = '0';
                menu.style.transform = 'translateY(-10px)';
                
                setTimeout(() => {
                    menu.style.opacity = '1';
                    menu.style.transform = 'translateY(0)';
                }, 10);
            });
        });
        
        // User menu enhancements
        const userMenuToggle = document.querySelector('.user-menu-toggle');
        if (userMenuToggle) {
            userMenuToggle.addEventListener('mouseenter', function() {
                this.style.transform = 'translateY(-2px)';
            });
            
            userMenuToggle.addEventListener('mouseleave', function() {
                this.style.transform = 'translateY(0)';
            });
        }
        
        // Admin button pulse effect
        const adminBtn = document.querySelector('.admin-btn');
        if (adminBtn) {
            adminBtn.addEventListener('mouseenter', function() {
                this.style.animation = 'pulse 0.6s infinite';
            });
            
            adminBtn.addEventListener('mouseleave', function() {
                this.style.animation = 'none';
            });
        }
        
        // Notification badge click handler
        const notificationBtn = document.querySelector('.btn-outline-secondary');
        if (notificationBtn) {
            notificationBtn.addEventListener('click', function() {
                // Show notifications panel or redirect
                showToast('Notifications feature coming soon!', 'info');
            });
        }
        
        // Brand logo hover effect
        const brandLogo = document.querySelector('.brand-logo');
        if (brandLogo) {
            brandLogo.addEventListener('mouseenter', function() {
                this.style.transform = 'scale(1.1) rotate(5deg)';
            });
            
            brandLogo.addEventListener('mouseleave', function() {
                this.style.transform = 'scale(1) rotate(0deg)';
            });
        }
        
        // Top notification bar interactions
        const topBar = document.querySelector('.top-notification-bar');
        if (topBar) {
            // Add close button functionality
            const closeBtn = document.createElement('button');
            closeBtn.className = 'btn-close btn-close-white btn-sm ms-2';
            closeBtn.setAttribute('aria-label', 'Close notification');
            closeBtn.addEventListener('click', function() {
                topBar.style.display = 'none';
            });
            
            const supportLink = topBar.querySelector('a[href^="mailto:"]');
            if (supportLink) {
                supportLink.parentNode.appendChild(closeBtn);
            }
        }
        
        // Enhanced mobile navigation
        const navbarToggler = document.querySelector('.navbar-toggler');
        const navbarCollapse = document.querySelector('.navbar-collapse');
        
        if (navbarToggler && navbarCollapse) {
            navbarToggler.addEventListener('click', function() {
                // Add animation class
                navbarCollapse.classList.add('collapsing');
                
                setTimeout(() => {
                    navbarCollapse.classList.remove('collapsing');
                }, 350);
            });
        }
        
        // Search suggestions (placeholder for future implementation)
        if (searchInput) {
            searchInput.addEventListener('input', function() {
                const query = this.value.trim();
                if (query.length > 2) {
                    // Show search suggestions
                    showSearchSuggestions(query);
                } else {
                    hideSearchSuggestions();
                }
            });
        }
        
        function showSearchSuggestions(query) {
            // Placeholder for search suggestions
            // This would typically fetch from an API and display suggestions
            console.log('Showing suggestions for:', query);
        }
        
        function hideSearchSuggestions() {
            // Hide search suggestions
            console.log('Hiding suggestions');
        }
        
        // Enhanced toast notifications
        function showToast(message, type = 'info') {
            const toast = document.createElement('div');
            toast.className = `alert alert-${type} alert-dismissible fade show position-fixed`;
            toast.style.cssText = 'top: 20px; right: 20px; z-index: 9999; min-width: 300px;';
            toast.innerHTML = `
                <i class="bi bi-${type === 'success' ? 'check-circle' : type === 'error' ? 'exclamation-triangle' : 'info-circle'} me-2"></i>
                ${message}
                <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
            `;
            
            document.body.appendChild(toast);
            
            // Auto-remove after 5 seconds
            setTimeout(() => {
                if (toast.parentNode) {
                    toast.remove();
                }
            }, 5000);
        }
        
        // Add CSS for enhanced animations
        const style = document.createElement('style');
        style.textContent = `
            .navbar {
                transition: transform 0.3s ease, background-color 0.3s ease;
            }
            
            .navbar-scrolled {
                background: rgba(255, 255, 255, 0.98) !important;
                backdrop-filter: blur(25px);
            }
            
            .dropdown-menu {
                transition: opacity 0.3s ease, transform 0.3s ease;
            }
            
            .search-focused .search-input {
                border-color: #667eea;
                box-shadow: 0 0 0 0.2rem rgba(102, 126, 234, 0.25);
            }
            
            .spin {
                animation: spin 1s linear infinite;
            }
            
            @keyframes spin {
                from { transform: rotate(0deg); }
                to { transform: rotate(360deg); }
            }
            
            .navbar-collapse.collapsing {
                transition: height 0.35s ease;
            }
            
            .btn-close {
                transition: all 0.3s ease;
            }
            
            .btn-close:hover {
                transform: scale(1.2);
            }
        `;
        document.head.appendChild(style);
    });

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

// Enhanced form validation
function validateForm(formElement) {
    const inputs = formElement.querySelectorAll('input[required], select[required], textarea[required]');
    let isValid = true;
    
    inputs.forEach(input => {
        if (!input.value.trim()) {
            input.classList.add('is-invalid');
            isValid = false;
        } else {
            input.classList.remove('is-invalid');
            input.classList.add('is-valid');
        }
    });
    
    return isValid;
}

// Enhanced image loading with skeleton
function loadImageWithSkeleton(imgElement, src) {
    const skeleton = document.createElement('div');
    skeleton.className = 'card-skeleton';
    skeleton.style.cssText = 'width: 100%; height: 300px; border-radius: 1.5rem 1.5rem 0 0;';
    
    imgElement.parentNode.insertBefore(skeleton, imgElement);
    imgElement.style.display = 'none';
    
    imgElement.onload = function() {
        skeleton.remove();
        imgElement.style.display = 'block';
        imgElement.style.opacity = '0';
        
        setTimeout(() => {
            imgElement.style.transition = 'opacity 0.5s ease';
            imgElement.style.opacity = '1';
        }, 100);
    };
    
    imgElement.onerror = function() {
        skeleton.remove();
        imgElement.src = '/images/no-cover.png';
        imgElement.style.display = 'block';
    };
    
    imgElement.src = src;
}

// Enhanced card hover effects
function enhanceCardHoverEffects() {
    const cards = document.querySelectorAll('.book-card, .author-card, .review-card');
    
    cards.forEach(card => {
        card.addEventListener('mouseenter', function() {
            this.style.transform = 'translateY(-8px) scale(1.02)';
            this.style.boxShadow = '0 12px 40px rgba(102,126,234,0.25)';
        });
        
        card.addEventListener('mouseleave', function() {
            this.style.transform = 'translateY(0) scale(1)';
            this.style.boxShadow = '0 4px 20px rgba(0,0,0,0.08)';
        });
    });
}

// Initialize enhanced features when DOM is loaded
document.addEventListener('DOMContentLoaded', function() {
    enhanceCardHoverEffects();
    
    // Enhanced loading states for buttons
    const buttons = document.querySelectorAll('.btn');
    buttons.forEach(button => {
        button.addEventListener('click', function() {
            if (this.type === 'submit' && !this.disabled) {
                const originalText = this.innerHTML;
                this.innerHTML = '<i class="bi bi-arrow-clockwise spin me-2"></i>Loading...';
                this.disabled = true;
                
                // Re-enable after form submission (this is a simplified version)
                setTimeout(() => {
                    this.innerHTML = originalText;
                    this.disabled = false;
                }, 2000);
            }
        });
    });

    // Enhanced social media interactions
    const socialLinks = document.querySelectorAll('.social-icons a');
    socialLinks.forEach(link => {
        link.addEventListener('click', function(e) {
            // Add click animation
            this.style.transform = 'scale(0.9)';
            setTimeout(() => {
                this.style.transform = 'scale(1.1)';
            }, 100);
            
            // Track social media clicks (placeholder for analytics)
            const platform = this.getAttribute('aria-label').toLowerCase();
            console.log(`User clicked on ${platform} link`);
            
            // Optional: Add analytics tracking here
            // trackSocialMediaClick(platform, this.href);
        });
        
        // Enhanced hover effects
        link.addEventListener('mouseenter', function() {
            this.style.transform = 'translateY(-3px) scale(1.1)';
        });
        
        link.addEventListener('mouseleave', function() {
            this.style.transform = 'translateY(0) scale(1)';
        });
    });
    
    // Social media analytics tracking function (placeholder)
    function trackSocialMediaClick(platform, url) {
        // This would typically send data to Google Analytics or similar
        // Example: gtag('event', 'social_media_click', { platform: platform, url: url });
        console.log(`Analytics: Social media click tracked for ${platform}`);
    }
});
