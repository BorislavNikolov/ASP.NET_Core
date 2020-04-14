let increaseGrappling = document.getElementById('incr-grappling');
let decreaseGrappling = document.getElementById('decr-grappling');

let grapplingValue = document.getElementById('grappling-value');
let grapplingBar = document.getElementById('grappling-bar');
increaseGrappling.addEventListener('click', () => {
    if (grapplingValue.value < 100 || grapplingBar.textContent != '100') {
        grapplingValue.value++;
        grapplingBar.textContent = Number(grapplingBar.textContent) + 1;
        grapplingBar.style.width = (Number(grapplingBar.style.width.replace('%', '')) + 1) + '%';
    }
});

decreaseGrappling.addEventListener('click', () => {
    if (grapplingValue.value > 65 || grapplingBar.textContent != '65') {
        grapplingValue.value--;
        grapplingBar.textContent = Number(grapplingBar.textContent) - 1;
        grapplingBar.style.width = (Number(grapplingBar.style.width.replace('%', '')) - 1) + '%';
    }
});

let increaseStriking = document.getElementById('incr-striking');
let decreaseStriking = document.getElementById('decr-striking');

let strikingValue = document.getElementById('striking-value');
let strikingBar = document.getElementById('striking-bar');
increaseStriking.addEventListener('click', () => {
    if (strikingValue.value < 100 || strikingBar.textContent != '100') {
        strikingValue.value++;
        strikingBar.textContent = Number(strikingBar.textContent) + 1;
        strikingBar.style.width = (Number(strikingBar.style.width.replace('%', '')) + 1) + '%';
    }
});

decreaseStriking.addEventListener('click', () => {
    if (strikingValue.value > 65 || strikingBar.textContent != '65') {
        strikingValue.value--;
        strikingBar.textContent = Number(strikingBar.textContent) - 1;
        strikingBar.style.width = (Number(strikingBar.style.width.replace('%', '')) - 1) + '%';
    }
});

let increaseWrestling = document.getElementById('incr-wrestling');
let decreaseWrestling = document.getElementById('decr-wrestling');

let wrestlingValue = document.getElementById('wrestling-value');
let wrestlingBar = document.getElementById('wrestling-bar');
increaseWrestling.addEventListener('click', () => {
    if (wrestlingValue.value < 100 || wrestlingBar.textContent != '100') {
        wrestlingValue.value++;
        wrestlingBar.textContent = Number(wrestlingBar.textContent) + 1;
        wrestlingBar.style.width = (Number(wrestlingBar.style.width.replace('%', '')) + 1) + '%';
    }
});

decreaseWrestling.addEventListener('click', () => {
    if (wrestlingValue.value > 65 || wrestlingBar.textContent != '65') {
        wrestlingValue.value--;
        wrestlingBar.textContent = Number(wrestlingBar.textContent) - 1;
        wrestlingBar.style.width = (Number(wrestlingBar.style.width.replace('%', '')) - 1) + '%';
    }
});

let increaseHealth = document.getElementById('incr-health');
let decreaseHealth = document.getElementById('decr-health');

let healthValue = document.getElementById('health-value');
let healthBar = document.getElementById('health-bar');
increaseHealth.addEventListener('click', () => {
    if (healthValue.value < 100 || healthBar.textContent != '100') {
        healthValue.value++;
        healthBar.textContent = Number(healthBar.textContent) + 1;
        healthBar.style.width = (Number(healthBar.style.width.replace('%', '')) + 1) + '%';
    }
});

decreaseHealth.addEventListener('click', () => {
    if (healthValue.value > 65 || healthBar.textContent != '65') {
        healthValue.value--;
        healthBar.textContent = Number(healthBar.textContent) - 1;
        healthBar.style.width = (Number(healthBar.style.width.replace('%', '')) - 1) + '%';
    }
});

let increaseStrenght = document.getElementById('incr-strenght');
let decreaseStrenght = document.getElementById('decr-strenght');

let strenghtValue = document.getElementById('strenght-value');
let strenghtBar = document.getElementById('strenght-bar');
increaseStrenght.addEventListener('click', () => {
    if (strenghtValue.value < 100 || strenghtBar.textContent != '100') {
        strenghtValue.value++;
        strenghtBar.textContent = Number(strenghtBar.textContent) + 1;
        strenghtBar.style.width = (Number(strenghtBar.style.width.replace('%', '')) + 1) + '%';
    }
});

decreaseStrenght.addEventListener('click', () => {
    if (strenghtValue.value > 65 || strenghtBar.textContent != '65') {
        strenghtValue.value--;
        strenghtBar.textContent = Number(strenghtBar.textContent) - 1;
        strenghtBar.style.width = (Number(strenghtBar.style.width.replace('%', '')) - 1) + '%';
    }
});

let increaseStamina = document.getElementById('incr-stamina');
let decreaseStamina = document.getElementById('decr-stamina');

let staminaValue = document.getElementById('stamina-value');
let staminaBar = document.getElementById('stamina-bar');
increaseStamina.addEventListener('click', () => {
    if (staminaValue.value < 100 || staminaBar.textContent != '100') {
        staminaValue.value++;
        staminaBar.textContent = Number(staminaBar.textContent) + 1;
        staminaBar.style.width = (Number(staminaBar.style.width.replace('%', '')) + 1) + '%';
    }
});

decreaseStamina.addEventListener('click', () => {
    if (staminaValue.value > 65 || staminaBar.textContent != '65') {
        staminaValue.value--;
        staminaBar.textContent = Number(staminaBar.textContent) - 1;
        staminaBar.style.width = (Number(staminaBar.style.width.replace('%', '')) - 1) + '%';
    }
});