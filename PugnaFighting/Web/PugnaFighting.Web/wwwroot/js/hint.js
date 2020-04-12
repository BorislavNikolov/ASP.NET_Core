let hintButton = document.getElementById('hint-btn');
let hintDiv = document.getElementById('hint');
let hintText = document.getElementById('hint-text');
let closeButton = document.getElementById('close-btn');

const hints = ['More fans - more earnings.',
    'Hire Cutman to improve your health points.',
    'Coaches help you a lot to win a fights.',
    'Better Manager means more profit',
    'Train your fighters frequently. They make your incomes.']

hintButton.addEventListener('click', () => {
    hintDiv.style.width = '20%';

    hintButton.style.display = 'none';
    hintDiv.style.display = 'block';
    closeButton.style.display = 'block';
    hintText.style.display = 'block';

    let index = Number(Math.floor(getRandom(0, hints.length)));
    let text = hints[index];

    hintText.textContent = text;
});

closeButton.addEventListener('click', () => {
    hintButton.style.display = 'block';
    hintText.style.display = 'none';
    closeButton.style.display = 'none';
    hintDiv.style.display = 'none';
})

function getRandom(min, max) {
    return Math.random() * (max - min) + min;
}