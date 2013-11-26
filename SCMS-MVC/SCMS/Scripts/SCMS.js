function FadeIn(ElementName) {
    $(ElementName).fadeIn(1000);
}

function FadeOut(ElementName, Time) {
    $(ElementName).fadeOut(7000);
}
function ValueMinus(id) {
    var val = $('#' + id).val();
    if (parseInt(val) > 1) {
        document.getElementById(id).value = parseInt(val) - 1;
    }
}
function ValuePlus(id) {
    var val = $('#' + id).val();
    if (parseInt(val) < 6) {
        document.getElementById(id).value = parseInt(val) + 1;
    }
}

(function ($) {
    $.widget("ui.combobox", {
        _create: function () {
            var input,
					that = this,
					wasOpen = false,
					select = this.element.hide(),
					selected = select.children(":selected"),
					value = selected.val() ? selected.text() : "",
					wrapper = this.wrapper = $("<span>")
						.addClass("ui-combobox")
						.insertAfter(select);

            function removeIfInvalid(element) {
                var value = $(element).val(),
						matcher = new RegExp("^" + $.ui.autocomplete.escapeRegex(value) + "$", "i"),
						valid = false;
                select.children("option").each(function () {
                    if ($(this).text().match(matcher)) {
                        this.selected = valid = true;
                        return false;
                    }
                });

                if (!valid) {
                    // remove invalid value, as it didn't match anything
                    $(element)
							.val("")
							.attr("title", value + " didn't match any item")
							.tooltip("open");
                    select.val("");
                    setTimeout(function () {
                        input.tooltip("close").attr("title", "");
                    }, 2500);
                    input.data("ui-autocomplete").term = "";
                }
            }

            input = $("<input>")
					.appendTo(wrapper)
					.val(value)
					.attr("title", "")
					.addClass("ui-state-default ui-combobox-input")
					.autocomplete({
					    delay: 0,
					    minLength: 0,
					    source: function (request, response) {
					        var matcher = new RegExp($.ui.autocomplete.escapeRegex(request.term), "i");
					        response(select.children("option").map(function () {
					            var text = $(this).text();
					            if (this.value && (!request.term || matcher.test(text)))
					                return {
					                    label: text.replace(
											new RegExp(
												"(?![^&;]+;)(?!<[^<>]*)(" +
												$.ui.autocomplete.escapeRegex(request.term) +
												")(?![^<>]*>)(?![^&;]+;)", "gi"
											), "<strong>$1</strong>"),
					                    value: text,
					                    option: this
					                };
					        }));
					    },
					    select: function (event, ui) {
					        ui.item.option.selected = true;
					        that._trigger("selected", event, {
					            item: ui.item.option
					        });
					    },
					    change: function (event, ui) {
					        if (!ui.item) {
					            removeIfInvalid(this);
					        }
					    }
					})
					.addClass("ui-widget ui-widget-content ui-corner-left");

            input.data("ui-autocomplete")._renderItem = function (ul, item) {
                return $("<li>")
						.append("<a>" + item.label + "</a>")
						.appendTo(ul);
            };

            $("<a>")
					.attr("tabIndex", -1)
					.attr("title", "Show All Items")
					.tooltip()
					.appendTo(wrapper)
					.button({
					    icons: {
					        primary: "ui-icon-triangle-1-s"
					    },
					    text: false
					})
					.removeClass("ui-corner-all")
					.addClass("ui-corner-right ui-combobox-toggle")
					.mousedown(function () {
					    wasOpen = input.autocomplete("widget").is(":visible");
					})
					.click(function () {
					    input.focus();

					    // close if already visible
					    if (wasOpen) {
					        return;
					    }

					    // pass empty string as value to search for, displaying all results
					    input.autocomplete("search", "");
					});

            input.tooltip({
                tooltipClass: "ui-state-highlight"
            });
        },

        _destroy: function () {
            this.wrapper.remove();
            this.element.show();
        }
    });
})(jQuery);

function GetMessage(MessageType) {
    if (MessageType == "SaveError") {
        return "Unable to save record!";
    }
    else if (MessageType == "DeleteError") {
        return "Unable to delete record!";
    }
    else if (MessageType == "DuplicateError") {
        return "Description should not be duplicate!";
    }
    else if (MessageType == "UnkownError") {
        return "An unknown error occured!";
    }
    else if (MessageType == "SaveSuccess") {
        return "Record saved successfully!";
    }
    else if (MessageType == "DeleteSuccess") {
        return "Record deleted successfully!";
    }
    return "";
}