function formErrorHandler(event, result) {
    let spanElement = document.querySelector(`[data-valmsg-for="${event.target.name}"]`)

    if (result) {
        event.target.classList.remove('input-validation-error');
        spanElement.classList.remove('field-validation-error');
        spanElement.classList.add('field-validation-valid');
        spanElement.innerHTML = '';
    }
    else {
        event.target.classList.add('input-validation-error');
        spanElement.classList.add('field-validation-error');
        spanElement.classList.remove('field-validation-valid');
        spanElement.innerHTML = event.target.dataset.valRequired;
    }
}

function lengthControl(event, minLength = 2) {
    if (event.target.value.length < minLength)
        return false

    return true;
}

function comparer(event, compareValue) {
    if (event !== compareValue)
        return false;

    return true;
}

function checkedControl(event) {
    if (event.target.checked)
        return true;

    return false;
}

function validateText(event) {
    formErrorHandler(event, lengthControl(event));
}

function checkboxValidation(event) {
    formErrorHandler(event, checkedControl(event));
}

function emailValidation(event) {
    const regEx = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
    formErrorHandler(event, regEx.test(event.target.value));
}

function passwordValidation(event) {
    if (event.target.dataset.valEqualtoOther !== undefined) {
        let compareValue = document.getElementsByName(event.target.dataset.valEqualtoOther.replace('*', 'Form'))[0].value;

        formErrorHandler(event, comparer(event.target.value, compareValue))
    }
    else {
        const regEx = /^(?=.*\d)(?=.*[!@#$%^&*])(?=.*[a-z])(?=.*[A-Z]).{8,}$/;
        formErrorHandler(event, regEx.test(event.target.value));
    }
}

let forms = document.querySelectorAll('form');
let inputs = forms[0].querySelectorAll('input');

inputs.forEach(input => {

    if (input.dataset.val === 'true') {

        if (input.type === 'checkbox') {
            input.addEventListener('change', (event) => {
                checkboxValidation(event);
            });
        }
        else {

       
            input.addEventListener('keyup', (event) => {

            input.addEventListener('keyup', (event) => {

                switch (event.target.type) {

                    case 'text':
                        validateText(event);
                        break;

                    case 'email':
                        emailValidation(event);
                        break;

                    case 'password':
                        passwordValidation(event);
                        break;
                }
            });    
        }
    }
});