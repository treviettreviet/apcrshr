appUtil = function ($) {

    var pleaseWaitDiv = $('<div class="modal hide" id="pleaseWaitDialog" data-backdrop="static" data-keyboard="false"><div class="modal-header"><h1>Processing...</h1></div><div class="modal-body"><div class="progress progress-striped active"><div class="bar" style="width: 100%;"></div></div></div></div>');

    Number.prototype.format = function (n, x) {
        var re = '\\d(?=(\\d{' + (x || 3) + '})+' + (n > 0 ? '\\.' : '$') + ')';
        return this.toFixed(Math.max(0, ~~n)).replace(new RegExp(re, 'g'), '$&,');
    };

    return {
        utility: {
            placeHolderControl: function () {
                var placeholder = null;
                $('input[type=text]').focus(function () {
                    placeholder = $(this).attr("placeholder");
                    $(this).attr("placeholder", "");
                });
                $('input[type=text]').blur(function () {
                    $(this).attr("placeholder", placeholder);
                });
            },

            getURLParameter: function (name) {
                return decodeURIComponent((new RegExp('[?|&]' + name + '=' + '([^&;]+?)(&|#|;|$)').exec(location.search) || [, ""])[1].replace(/\+/g, '%20')) || null
            },

            
            validateNumber: function (control) {
                var value = $(control).val();
                var RE = /^-{0,1}\d*\.{0,1}\d+$/;
                return (RE.test(value));
            },

        }
    };
}(jQuery);