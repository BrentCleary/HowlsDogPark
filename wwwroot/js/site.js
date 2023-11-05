// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// script.js
document.addEventListener("DOMContentLoaded", function () {
  const floatingBar = document.querySelector(".floating-bar");

  // Function to show the floating bar
  function showFloatingBar() {
      const screenHeight = window.innerHeight;
      const scrollY = window.scrollY;

      if (scrollY > screenHeight) {
          floatingBar.style.bottom = "0";
      } else {
          floatingBar.style.bottom = "-50px";
      }
  }

  // Add scroll event listener
  window.addEventListener("scroll", showFloatingBar);

  // Initial check on page load
  showFloatingBar();
});



function DeleteCheck()
{
    confirm("Do you want to delete this dog?");
}




// TODO: Update Slider value on all Dog Inputs and Displays

var slider = document.getElementById("slider");
var sliderValue = document.getElementById("slider-value");
var temperamentInput = document.getElementById("temperament-input");
var sliderText = ["Fearful/Aggressive", "Shy/Timid", "Calm", "Friendly", "Happy/Playful"];

// Update the input value when the slider changes
slider.oninput = function () {
    // Update the slider value display
    var sliderText = ["Fearful/Aggressive", "Shy/Timid", "Calm", "Friendly", "Happy/Playful"];
    sliderValue.textContent = sliderText[slider.value - 1];

    // Update the input value
    temperamentInput.value = slider.value;
};



// JavaScript to populate available times in 30-minute intervals
const timeInput = document.getElementById("timeInput");

// Function to create an array of time options in HH:mm format
function createTimeOptions() {
    const options = [];
    for (let hour = 0; hour < 24; hour++) {
        for (let minute = 0; minute < 60; minute += 30) {
            const formattedHour = hour.toString().padStart(2, "0");
            const formattedMinute = minute.toString().padStart(2, "0");
            options.push(`${formattedHour}:${formattedMinute}`);
        }
    }
    return options;
}

// Populate the select input with time options
function populateTimeOptions() {
    const timeOptions = createTimeOptions();
    timeInput.innerHTML = "";
    timeOptions.forEach(option => {
        const timeOption = document.createElement("option");
        timeOption.value = option;
        timeOption.text = option;
        timeInput.appendChild(timeOption);
    });
}

// Call the function to populate time options
populateTimeOptions();
