import {useState, useEffect} from 'react';
import './FishCrud.css';
import FishForm from './FishForm.js';
import FishTable from './FishTable.js';

export default
function FishCrud() {
  const [fish, setFish] = useState({
    id: 0,
    name: "",
    color: "",
    isAggressive: false,
    aquariumId: ""
  });

  const [fishes, setFishes] = useState([]);

  useEffect(() => {
    const storedFishes = JSON.parse(window.localStorage.getItem("fishes")) || [];
    setFishes(storedFishes);
  }, []);

  function handleChange(event) {
    const { name, value, type, checked } = event.target;

    const fieldValue = type === "checkbox" ? checked : value;

    setFish((previousFish) => {
      return { ...previousFish, [name]: fieldValue };
    });
  }

  function addFish() {
    const storedFishes = JSON.parse(window.localStorage.getItem("fishes")) || [];

    const fishExists = storedFishes.some(item => item.id === fish.id);
    if (fishExists) {
      alert("Fish with this ID already exists.");
      return;
    }

    const updatedFishes = [...storedFishes, fish];
    window.localStorage.setItem("fishes", JSON.stringify(updatedFishes));
    setFishes(updatedFishes);

    window.localStorage.setItem(fish.id, JSON.stringify(fish));

    console.log(fish);
    console.log(JSON.stringify(updatedFishes));
  }

  return (
    <div id="tableFormContainer">
      <FishTable fishes={fishes} updateFish={updateFish} deleteFish={deleteFish}/>
      <FishForm fish={fish} handleChange={handleChange} addFish={addFish} />
    </div>
  );

  function updateFish(fishId) {
    const storedFishes = JSON.parse(window.localStorage.getItem("fishes")) || [];

    let currentFishIndex = storedFishes.findIndex(f => f.id === fishId);
    let currentFish = storedFishes[currentFishIndex];

    if (!currentFish) {
      console.error("Fish not found in the array.");
      return;
    }

    let nameInput = prompt("Update name:", currentFish.name);
    let colorInput = prompt("Update color:", currentFish.color);
    let isAggressiveInput = prompt("Update aggressiveness (true/false):", currentFish.isAggressive ? "true" : "false");
    let aquariumIdInput = prompt("New aquarium:", currentFish.aquariumId);

    if (!nameInput || !colorInput || !isAggressiveInput || !aquariumIdInput) {
      alert("All fields are required.");
      return;
    }

    currentFish.name = nameInput;
    currentFish.color = colorInput;
    currentFish.isAggressive = isAggressiveInput === "true";
    currentFish.aquariumId = aquariumIdInput;

    storedFishes[currentFishIndex] = currentFish;

    window.localStorage.setItem("fishes", JSON.stringify(storedFishes));

    window.localStorage.setItem(currentFish.id, JSON.stringify(currentFish));

    setFishes(storedFishes);

    console.log("Fish updated:", currentFish);
  }

  function deleteFish(fishId){
    const storedFishes = JSON.parse(window.localStorage.getItem("fishes")) || [];

    const updatedFishes = storedFishes.filter(fish => fish.id !== fishId);

    window.localStorage.setItem("fishes",JSON.stringify(updatedFishes));

    window.localStorage.removeItem(fishId);

    setFishes(updatedFishes);
  }
}
