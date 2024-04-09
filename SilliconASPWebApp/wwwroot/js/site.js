document.addEventListener('DOMContentLoaded', function () {
    DropDown()
    Search_Course()
    DarkLightMode_Switch()
})


//---------------------------------------------------------------------------------------

//mobile menu
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

//change to darkmode

function DarkLightMode_Switch() {
    let switchButton = document.querySelector('#switch-mode')
    switchButton.addEventListener('change', function () {
        try {

            let theme = this.checked ? "dark" : "light"

            fetch(`/sitesettings/changetheme?theme=${theme}`)
                .then(res => {
                    if (res.ok)
                        window.location.reload()
                    else
                        console.log("tjena")
                })
        }
        catch (error) { console.log(e.message) }
    })
}

//---------------------------------------------------------------------------------------

//update course function in admin page
const modifyBtn = document.getElementById('modifyBtn')
const updateBtn = document.getElementById('update-btn')
let form = document.getElementById('update-form')
let show = false

form.classList.add('hidden')

modifyBtn.addEventListener('click', function (event) {
    event.preventDefault(event);
    Hide_Show('show-form', 'hidden')
})

updateBtn.addEventListener('click', () => {
    Hide_Show('show-form', 'hidden')
})

function Hide_Show(showClass, hideClass) {

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

//dropdown in courses

function DropDown() {

    try {
        let selected = document.querySelector('.selected')
        let menu = document.querySelector('.menu')

        let selectOptions = document.querySelector('.select-options')

        menu.addEventListener('click', function () {
            selectOptions.style.display = (selectOptions.style.display === 'block') ? 'none' : 'block'
        })

        let options = selectOptions.querySelectorAll('.option')
        options.forEach(function (option) {
            option.addEventListener('click', function () {
                selected.innerHTML = this.textContent
                selectOptions.style.display = 'none'
                let category = this.getAttribute('data-value')
                selected.setAttribute('data-value', category)
                Filter_Courses()
            })
        })

    }
    catch (error) { console.log(error.message) }
}


function Search_Course(){
    try {
        const input = document.querySelector('#searchQuery')
        input.addEventListener('keyup', function () {
            Filter_Courses()
        })
    }
    catch (error) { console.log(error.message) }
}

function Filter_Courses() {
    const category = document.querySelector('.dropdown .selected').getAttribute('data-value') || 'all'
    const searchValue = document.querySelector('#searchQuery').value
    const URL = `/courses/index?category=${encodeURIComponent(category)}&searchValue=${encodeURIComponent(searchValue)}`

    try {

        fetch(URL)
            .then(res => res.text())
            .then(data => {
                const parser = new DOMParser()
                const dom = parser.parseFromString(data, 'text/html')
                document.querySelector('.course-cards').innerHTML = dom.querySelector('.course-cards').innerHTML
            })

    }
    catch (error) { console.log(error.message) }
}