// Việt hóa các thông báo validation cho phần Position
$(document).ready(function() {
    // Việt hóa các thông báo validation mặc định của jQuery Validation
    $.extend($.validator.messages, {
        required: "Trường này là bắt buộc.",
        remote: "Vui lòng sửa trường này.",
        email: "Vui lòng nhập địa chỉ email hợp lệ.",
        url: "Vui lòng nhập URL hợp lệ.",
        date: "Vui lòng nhập ngày hợp lệ.",
        dateISO: "Vui lòng nhập ngày hợp lệ (ISO).",
        number: "Vui lòng nhập số hợp lệ.",
        digits: "Vui lòng chỉ nhập chữ số.",
        creditcard: "Vui lòng nhập số thẻ tín dụng hợp lệ.",
        equalTo: "Vui lòng nhập lại giá trị này.",
        accept: "Vui lòng nhập giá trị có phần mở rộng hợp lệ.",
        maxlength: $.validator.format("Vui lòng nhập tối đa {0} ký tự."),
        minlength: $.validator.format("Vui lòng nhập ít nhất {0} ký tự."),
        rangelength: $.validator.format("Vui lòng nhập giá trị có độ dài từ {0} đến {1} ký tự."),
        range: $.validator.format("Vui lòng nhập giá trị từ {0} đến {1}."),
        max: $.validator.format("Vui lòng nhập giá trị nhỏ hơn hoặc bằng {0}."),
        min: $.validator.format("Vui lòng nhập giá trị lớn hơn hoặc bằng {0}.")
    });

    // Việt hóa các thông báo validation cho trường cụ thể
    $.validator.addMethod("positionNameRequired", function(value, element) {
        return this.optional(element) || value.trim().length > 0;
    }, "Tên chức vụ là bắt buộc.");

    $.validator.addMethod("dateRequired", function(value, element) {
        return this.optional(element) || value.trim().length > 0;
    }, "Ngày hiệu lực là bắt buộc.");

    $.validator.addMethod("validDecimal", function(value, element) {
        if (this.optional(element)) {
            return true;
        }
        // Kiểm tra định dạng số thập phân (cho phép dấu chấm hoặc dấu phẩy)
        return /^\d*([.,]\d+)?$/.test(value);
    }, "Vui lòng nhập số thập phân hợp lệ.");

    // Thêm class validation cho các trường
    if ($('#createForm').length > 0 || $('#editForm').length > 0) {
        // Validation cho form Create và Edit
        var formId = $('#createForm').length > 0 ? '#createForm' : '#editForm';
        
        $(formId).validate({
            rules: {
                'Name': {
                    required: true,
                    minlength: 2,
                    maxlength: 100
                },
                'ShortName': {
                    required: true,
                    minlength: 1,
                    maxlength: 50
                },
                'TypeObject': {
                    required: true
                },
                'DateIssued': {
                    required: true
                },
                'BasicPosiontConfficient': {
                    validDecimal: true,
                    min: 0
                },
                'ResponsibilityCoefficient': {
                    validDecimal: true,
                    min: 0
                },
                'PosiontConfficient': {
                    validDecimal: true,
                    min: 0
                },
                'Description': {
                    maxlength: 500
                },
                'Note': {
                    maxlength: 500
                }
            },
            messages: {
                'Name': {
                    required: "Vui lòng nhập tên chức vụ.",
                    minlength: "Tên chức vụ phải có ít nhất 2 ký tự.",
                    maxlength: "Tên chức vụ không được vượt quá 100 ký tự."
                },
                'ShortName': {
                    required: "Vui lòng nhập tên viết tắt.",
                    minlength: "Tên viết tắt phải có ít nhất 1 ký tự.",
                    maxlength: "Tên viết tắt không được vượt quá 50 ký tự."
                },
                'TypeObject': {
                    required: "Vui lòng chọn loại đối tượng."
                },
                'DateIssued': {
                    required: "Vui lòng chọn ngày hiệu lực."
                },
                'BasicPosiontConfficient': {
                    validDecimal: "Vui lòng nhập hệ số cơ bản hợp lệ.",
                    min: "Hệ số cơ bản phải lớn hơn hoặc bằng 0."
                },
                'ResponsibilityCoefficient': {
                    validDecimal: "Vui lòng nhập hệ số trách nhiệm hợp lệ.",
                    min: "Hệ số trách nhiệm phải lớn hơn hoặc bằng 0."
                },
                'PosiontConfficient': {
                    validDecimal: "Vui lòng nhập hệ số chức vụ hợp lệ.",
                    min: "Hệ số chức vụ phải lớn hơn hoặc bằng 0."
                },
                'Description': {
                    maxlength: "Mô tả không được vượt quá 500 ký tự."
                },
                'Note': {
                    maxlength: "Ghi chú không được vượt quá 500 ký tự."
                }
            },
            errorPlacement: function(error, element) {
                // Hiển thị lỗi bên dưới trường input
                error.insertAfter(element);
            },
            highlight: function(element) {
                // Thêm class lỗi khi trường có lỗi
                $(element).addClass('is-invalid');
            },
            unhighlight: function(element) {
                // Xóa class lỗi khi trường không còn lỗi
                $(element).removeClass('is-invalid');
            },
            errorClass: 'text-danger',
            validClass: 'is-valid'
        });
    }

    // Xử lý hiển thị thông báo lỗi tùy chỉnh
    function showCustomError(message, isError = true) {
        var alertClass = isError ? 'alert-danger' : 'alert-success';
        var alertHtml = '<div class="alert ' + alertClass + ' alert-dismissible fade show" role="alert">' +
                       '<i class="fa fa-' + (isError ? 'exclamation-triangle' : 'check-circle') + '"></i> ' +
                       message +
                       '<button type="button" class="close" data-dismiss="alert" aria-label="Close">' +
                       '<span aria-hidden="true">&times;</span>' +
                       '</button>' +
                       '</div>';
        
        // Xóa thông báo cũ nếu có
        $('.alert').remove();
        
        // Thêm thông báo mới vào đầu form
        var form = $('#createForm').length > 0 ? $('#createForm') : $('#editForm');
        if (form.length > 0) {
            form.prepend(alertHtml);
        }
    }

    // Xử lý sự kiện submit form
    $('form').on('submit', function(e) {
        var form = $(this);
        var formId = form.attr('id');
        
        if (formId === 'createForm' || formId === 'editForm') {
            // Kiểm tra validation trước khi submit
            if (!form.valid()) {
                e.preventDefault();
                showCustomError('Vui lòng kiểm tra và sửa các lỗi trong form trước khi gửi.');
                return false;
            }
            
            // Kiểm tra tên chức vụ trùng lặp
            var positionName = $('#Name').val().trim();
            var shortName = $('#ShortName').val().trim();
            
            if (positionName === '') {
                e.preventDefault();
                showCustomError('Vui lòng nhập tên chức vụ.');
                $('#Name').focus();
                return false;
            }
            
            if (shortName === '') {
                e.preventDefault();
                showCustomError('Vui lòng nhập tên viết tắt.');
                $('#ShortName').focus();
                return false;
            }
        }
    });

    // Xử lý sự kiện xóa chức vụ
    if ($('form[asp-action="Delete"]').length > 0) {
        $('form[asp-action="Delete"]').on('submit', function(e) {
            if (!confirm('Bạn có chắc chắn muốn xóa chức vụ này không? Hành động này không thể hoàn tác.')) {
                e.preventDefault();
                return false;
            }
        });
    }

    // Xử lý sự kiện thay đổi trạng thái
    if ($('form[asp-action="Deactivate"]').length > 0) {
        $('form[asp-action="Deactivate"]').on('submit', function(e) {
            var positionName = $(this).data('position-name') || 'chức vụ này';
            if (!confirm('Bạn có chắc chắn muốn đổi trạng thái của chức vụ "' + positionName + '" sang hết hiệu lực không?')) {
                e.preventDefault();
                return false;
            }
        });
    }
});
