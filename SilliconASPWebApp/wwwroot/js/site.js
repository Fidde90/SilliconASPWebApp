﻿const btnClicked = document.querySelector('.btn-mobile')
let m_menu = document.querySelector('#mobile-menu')
m_menu.classList.add('invisible')
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
    if (window.innerWidth >= 1200) {
        m_menu.classList.add('invisible')
        m_menu.classList.remove('mobile-menu')
    } else {
        if (!document.getElementById('mobile-menu').classList.contains('invisible')) {
            document.getElementById('mobile-menu').classList.add('invisible');
            m_menu.classList.remove('mobile-menu')       
        }
    }
}

window.addEventListener('resize', checkScreenSize);
checkScreenSize();
