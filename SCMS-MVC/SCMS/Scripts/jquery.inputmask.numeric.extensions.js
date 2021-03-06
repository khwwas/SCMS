/*
Input Mask plugin extensions
http://github.com/RobinHerbots/jquery.inputmask
Copyright (c) 2010 - 2012 Robin Herbots
Licensed under the MIT license (http://www.opensource.org/licenses/mit-license.php)
Version: 1.2.0

Optional extensions on the jquery.inputmask base
*/
(function ($) {
    //number aliases
    $.extend($.inputmask.defaults.aliases, {
        'decimal': {
            mask: "~",
            placeholder: "",
            repeat: 10,
            greedy: false,
            numericInput: true,
			digits: "*", //numer of digits
            groupSeparator: ",", // | "."
            groupSize: 3,
            autoGroup: false,
            regex: {
                number: function (groupSeparator, groupSize, radixPoint, digits) {
                	var escapedGroupSeparator = $.inputmask.escapeRegex.call(this, groupSeparator);
                	var escapedRadixPoint = $.inputmask.escapeRegex.call(this, radixPoint);
                	var digitExpression = isNaN(digits) ? digits : '{0,' + digits + '}'
                    return new RegExp("^[\+\-]?(\\d{1," + groupSize + "}" + escapedGroupSeparator + ")?((\\d{" + groupSize + "}" + escapedGroupSeparator + ")|\\d)*[" + escapedRadixPoint + "]?\\d" + digitExpression + "$");
                }
            },
            onKeyDown: function (e, opts) {
                var $input = $(this), input = this;
                if (e.keyCode == opts.keyCode.TAB) {
                    var nptStr = input._valueGet();
                    var radixPosition = nptStr.indexOf(opts.radixPoint);
                    if (radixPosition != -1) {
                        for (var i = 1; i < opts.digits; i++) {
                            if (nptStr[radixPosition + i]) nptStr = nptStr + "0";
                        }
                        if (nptStr !== $input.val()) {
                        	$input.val(nptStr);
                        }
                    }
                }
            },
            definitions: {
                '~': { //real number
                    validator: function (chrs, buffer, pos, strict, opts) {
                        var cbuf = buffer.slice();
                        cbuf.splice(pos, 0, chrs);
                        var bufferStr = cbuf.join('');
                        if (/^0[\d|-]$/.test(bufferStr)) { //handle first char
                            buffer[0]= "";
                            return { "pos": 1, "c": "" };
                        }
                        var isValid = opts.regex.number(opts.groupSeparator, opts.groupSize, opts.radixPoint, opts.digits).test(bufferStr);
                        if (!isValid) {
                            if (strict) { //shiftL & shiftR use strict only validate from 0 to position
                                var cbuf = buffer.slice(0, pos);
                                cbuf.splice(pos, 0, chrs);
                                var bufferStr = cbuf.join('');
                                var isValid = opts.regex.number(opts.groupSeparator, opts.groupSize, opts.radixPoint, opts.digits).test(bufferStr);
                            }
                            else {
                                if (bufferStr == opts.radixPoint) {
                                    isValid = opts.regex.number(opts.groupSeparator, opts.groupSize, opts.radixPoint, opts.digits).test("0" + bufferStr);
                                    if (isValid) {
                                        buffer[pos] = "0";
                                        pos++;
                                        return { "pos": pos };
                                    }
                                }
                            }
                        }
                        //grouping
                        if (opts.autoGroup && isValid != false && !strict) {
                            var bufVal = buffer.join('') + chrs;
                            bufVal = bufVal.replace(new RegExp("\\" + opts.groupSeparator, "g"), '');
                            var reg = new RegExp('(-?\\d+)(\\d{' + opts.groupSize + '})');
                            while (reg.test(bufVal)) {
                                bufVal = bufVal.replace(reg, '$1' + opts.groupSeparator + '$2');
                            }
                            for (var i = 0, l = bufVal.length - 1; i < l; i++) {
                                buffer[i] = bufVal.charAt(i);
                            }
                            buffer.length++;
                            return { "pos": buffer.length };
                        }

                        return isValid;
                    },
                    cardinality: 1,
                    prevalidator: null
                }
            },
            insertMode: true,
            autoUnmask: false
        },
        'non-negative-decimal': {
            regex: {
                 number: function (groupSeparator, groupSize, radixPoint, digits) {
                	var escapedGroupSeparator = $.inputmask.escapeRegex.call(this, groupSeparator);
                	var escapedRadixPoint = $.inputmask.escapeRegex.call(this, radixPoint);
                	var digitExpression = isNaN(digits) ? digits : '{0,' + digits + '}'
                     return new RegExp("^[\+]?(\\d{1," + groupSize + "}" + escapedGroupSeparator + ")?((\\d{" + groupSize + "}" + escapedGroupSeparator + ")|\\d)*[" + escapedRadixPoint + "]?\\d" + digitExpression + "$");
                }
            },
            alias: "decimal"
        },
        'integer': {
            regex: {
                number: function (groupSeparator, groupSize) { return new RegExp("^([\+\-]?\\d*)$"); }
            },
            alias: "decimal"
        }
    });
})(jQuery);
