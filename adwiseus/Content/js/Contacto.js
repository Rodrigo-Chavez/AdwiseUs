document.addEventListener('DOMContentLoaded', function () {
    const form = document.getElementById('contactForm');
    const inputName = document.getElementById('InputName');
    const inputEmail = document.getElementById('InputEmail');
    const inputMessage = document.getElementById('InputMessage');

    const nameError = document.getElementById('nameError');
    const emailError = document.getElementById('emailError');
    const messageError = document.getElementById('messageError');

    // Agregar escuchadores de eventos de entrada para validar mientras escribe
    inputName.addEventListener('input', validateName);
    inputEmail.addEventListener('input', validateEmailInput);
    inputMessage.addEventListener('input', validateMessage);

    form.addEventListener('submit', function (event) {
        let valid = true;

        // Resetear mensajes de error
        nameError.textContent = '';
        emailError.textContent = '';
        messageError.textContent = '';

        // Validar nombre
        if (inputName.value.trim() === '') {
            valid = false;
            nameError.textContent = 'El nombre es obligatorio.';
            inputName.classList.add('is-invalid');
        } else if (inputName.value.length > 50) {
            valid = false;
            nameError.textContent = 'El nombre no debe exceder los 50 caracteres.';
            inputName.classList.add('is-invalid');
        } else if (inputName.value.length < 2) {
            valid = false;
            nameError.textContent = 'El nombre debe tener al menos 2 caracteres.';
            inputName.classList.add('is-invalid');
        } else {
            inputName.classList.remove('is-invalid');
            inputName.classList.add('is-valid');
        }

        // Validar email
        if (inputEmail.value.trim() === '') {
            valid = false;
            emailError.textContent = 'El correo electrónico es obligatorio.';
            inputEmail.classList.add('is-invalid');
        } else if (inputEmail.value.length > 254) {
            valid = false;
            emailError.textContent = 'El correo electrónico no debe exceder los 254 caracteres.';
            inputEmail.classList.add('is-invalid');
        } else if (inputEmail.value.length < 5) {
            valid = false;
            emailError.textContent = 'El correo electrónico debe tener al menos 5 caracteres.';
            inputEmail.classList.add('is-invalid');
        } else if (!validateEmail(inputEmail.value.trim())) {
            valid = false;
            emailError.textContent = 'El correo electrónico no es válido.';
            inputEmail.classList.add('is-invalid');
        } else {
            inputEmail.classList.remove('is-invalid');
            inputEmail.classList.add('is-valid');
        }

        // Validar mensaje
        if (inputMessage.value.trim() === '') {
            valid = false;
            messageError.textContent = 'El mensaje es obligatorio.';
            inputMessage.classList.add('is-invalid');
        } else if (inputMessage.value.length > 1000) {
            valid = false;
            messageError.textContent = 'El mensaje no debe exceder los 1000 caracteres.';
            inputMessage.classList.add('is-invalid');
        } else if (inputMessage.value.length < 10) {
            valid = false;
            messageError.textContent = 'El mensaje debe tener al menos 10 caracteres.';
            inputMessage.classList.add('is-invalid');
        } else {
            inputMessage.classList.remove('is-invalid');
            inputMessage.classList.add('is-valid');
        }

        if (!valid) {
            event.preventDefault(); // Evitar el envío del formulario si hay errores de validación
        }
    });

    function validateEmail(email) {
        const re = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        return re.test(email);
    }

    function validateName() {
        if (inputName.value.trim() === '') {
            nameError.textContent = 'El nombre es obligatorio.';
            inputName.classList.remove('is-valid');
            inputName.classList.add('is-invalid');
        } else if (inputName.value.length > 50) {
            nameError.textContent = 'El nombre no debe exceder los 50 caracteres.';
            inputName.classList.remove('is-valid');
            inputName.classList.add('is-invalid');
        } else if (inputName.value.length < 2) {
            nameError.textContent = 'El nombre debe tener al menos 2 caracteres.';
            inputName.classList.remove('is-valid');
            inputName.classList.add('is-invalid');
        } else {
            nameError.textContent = '';
            inputName.classList.remove('is-invalid');
            inputName.classList.add('is-valid');
        }
    }

    function validateEmailInput() {
        if (inputEmail.value.trim() === '') {
            emailError.textContent = 'El correo electrónico es obligatorio.';
            inputEmail.classList.remove('is-valid');
            inputEmail.classList.add('is-invalid');
        } else if (inputEmail.value.length > 254) {
            emailError.textContent = 'El correo electrónico no debe exceder los 254 caracteres.';
            inputEmail.classList.remove('is-valid');
            inputEmail.classList.add('is-invalid');
        } else if (inputEmail.value.length < 5) {
            emailError.textContent = 'El correo electrónico debe tener al menos 5 caracteres.';
            inputEmail.classList.remove('is-valid');
            inputEmail.classList.add('is-invalid');
        } else if (!validateEmail(inputEmail.value.trim())) {
            emailError.textContent = 'El correo electrónico no es válido.';
            inputEmail.classList.remove('is-valid');
            inputEmail.classList.add('is-invalid');
        } else {
            emailError.textContent = '';
            inputEmail.classList.remove('is-invalid');
            inputEmail.classList.add('is-valid');
        }
    }

    function validateMessage() {
        if (inputMessage.value.trim() === '') {
            messageError.textContent = 'El mensaje es obligatorio.';
            inputMessage.classList.remove('is-valid');
            inputMessage.classList.add('is-invalid');
        } else if (inputMessage.value.length > 1000) {
            messageError.textContent = 'El mensaje no debe exceder los 1000 caracteres.';
            inputMessage.classList.remove('is-valid');
            inputMessage.classList.add('is-invalid');
        } else if (inputMessage.value.length < 10) {
            messageError.textContent = 'El mensaje debe tener al menos 10 caracteres.';
            inputMessage.classList.remove('is-valid');
            inputMessage.classList.add('is-invalid');
        } else {
            messageError.textContent = '';
            inputMessage.classList.remove('is-invalid');
            inputMessage.classList.add('is-valid');
        }
    }
});