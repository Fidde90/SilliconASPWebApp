let btnClicked = document.querySelector('.btn-mobile')
let m_menu = document.querySelector('#mobile-menu')
let active = false;

btnClicked.addEventListener('click', () => {
    active = !active

    if (active) {
        m_menu.classList.remove('invisible')
        m_menu.classList.add('mobile-menu')
        btnClicked.innerHTML = '<i class="fa-solid fa-xmark"></i>'
    } else {
        m_menu.classList.add('invisible')
        m_menu.classList.remove('mobile-menu')
        btnClicked.innerHTML = '<i class="fa-solid fa-bars-staggered"></i>'
    }
})

const checkScreenSize = () => {

    btnClicked.innerHTML = '<i class="fa-solid fa-bars-staggered"></i>'
    if (window.innerWidth >= 1200) {
        m_menu.classList.add('invisible')
        m_menu.classList.remove('mobile-menu')
        active = !active
    } else {

        if (!document.getElementById('mobile-menu').classList.contains('invisible')) {
            document.getElementById('mobile-menu').classList.add('invisible');
            m_menu.classList.remove('mobile-menu')
            active = !active
        }
    }
}

window.addEventListener('resize', checkScreenSize);
checkScreenSize();
//---------------------------------------------------------------------------------------

document.addEventListener('DOMContentLoaded', function () {
    let switchButton = document.querySelector('#switch-mode')
    switchButton.addEventListener('change', function () {
        let theme = this.checked ? "dark" : "light"

        fetch(`/sitesettings/changetheme?theme=${theme}`)
            .then(res => {
                if (res.ok)
                    window.location.reload()
                else
                    console.log("tjena")
            })
    })
})
//---------------------------------------------------------------------------------------

const modifyBtn = document.getElementById('modifyBtn')
const updateBtn = document.getElementById('update-btn')
let form = document.getElementById('update-form')
let show = false


form.classList.add('hidden')
modifyBtn.addEventListener('click', function (event) {
    event.preventDefault(event);
    HideShow('show-form','hidden')
})

updateBtn.addEventListener('click', () => {
    HideShow('show-form','hidden')
})

function HideShow(showClass, hideClass) {

    show = !show
    if (show === true) {
        form.classList.add(`${showClass}`)
        form.classList.remove(`${hideClass}`)
    } else {
        form.classList.add(`${hideClass}`)
        form.classList.remove(`${showClass}`)
    }
}
//---------------------------------------------------------------------------------------