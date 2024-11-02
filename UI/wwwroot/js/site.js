$(document).ready(function () {
    obtenerToken();
    var form = $("#registerForm");
    var button = $("#guardarContacto");
    initValidate();
    $("#guardarContacto").prop("disabled", true)
    button.on('click', function (event) {
        let token = localStorage.getItem('token')
        debugger
        let datos =JSON.stringify({
            companyName: $("#compania").val(),
            contact: $("#contacto").val(),
            email: $("#email").val(),
            phone: $("#telefono").val(),
            checkConditions: $("#terminos").is(":checked")
        })
        $.ajax({
            url: "https://localhost:7180/api/Register",
            type: 'POST',
            headers: { 'Authorization': 'Bearer ' + token },
            contentType: 'application/json; charset=utf-8',
            data: datos,
            dataType: 'json',  
            success: function (response) {
               
                $("#modalSuccess").modal("show");
                $("#textCompania").text(response.persona.companyName);
                $("#textContacto").text(response.persona.contact);
                $("#textEmail").text(response.persona.email);
                $("#textTelefono").text(response.persona.phone);
                form[0].reset();

            }
            , error: function (xhr, status, error) {
                console.error("Error al obtener los datos:", error);
                $("#modalError").modal("show");
                $("#textError").text(xhr.responseText);
            }
            

        })
    });
    function obtenerToken() {
        $.ajax({
            url:"https://localhost:7180/api/Auth/token",
            type: 'GET',
            dataType: "json",
            success: function (response) {
                localStorage.setItem("token",response.token)
            },error: function (error) {
                console.error("Error al obtener los datos:", error); // Maneja cualquier error
            }
        })
    }
    function checkForm() {
        let validate = $("#registerForm").valid()
        if (validate) {

            button.prop("disabled", false)

        } else {
            button.prop("disabled", true)

        }
    }
    $('#registerForm input').on('input', function () {
        checkForm();
    });
    $.validator.addMethod("phoneMX", function (phone_number, element) {
        phone_number = phone_number.replace(/\D/g, ''); 
        return this.optional(element) || (phone_number.length === 10); 
    }, "Ingrese un teléfono valido");

    function initValidate() {
        form.validate({
            rules: {
                compania: {
                    required: true
                },
                contacto: {
                    required: true
                },
                email: {
                    required: true,
                    email: true
                },
                telefono: {
                    required: true,
                    phoneMX: true
                },
                terminos: {
                    required: true,
                }
            },
            messages: {
                compania: {
                    required: "Ingrese el nombre de la compañia"
                },
                contacto: {
                    required: "Ingrese el nombre del contacto"
                },
                email: {
                    required: "Ingrese el email",
                    email: "Valide correctamente el email"
                },
                telefono: {
                    required: "Ingrese el telefono",
                    phoneMX: "El numero solo debe tener 10 dígitos"
                },
                terminos: {
                    required: "Acepte los terminos y condiciones"
                }
            },
            errorPlacement: function (error, element) {
                error.insertAfter(element);
            },
        });
    }
});
