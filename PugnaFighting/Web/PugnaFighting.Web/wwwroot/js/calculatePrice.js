let price = document.getElementById('price');

let incrementButtons = document.getElementsByClassName('increment');
let decrementButtons = document.getElementsByClassName('decrement');

[...incrementButtons].forEach(button => button.addEventListener('click', () => {
    price.value = Number(price.value) + 1000;
}));

[...decrementButtons].forEach(button => button.addEventListener('click', () => {
    price.value -= 1000;
}));