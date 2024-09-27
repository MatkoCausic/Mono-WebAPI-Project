import {useState} from 'react';
import FishTable from './FishTable.js';
import FishForm from './FishForm.js';

export default
function FishTableForm(){
    const [fishes,setFishes] = useState([]);
    const [fish,setFish] = useState({
        id:1,
        name:"",
        color:"",
        isAggressive:false,
        aquariumId:""
    });

    function addFish(){
        const storedFishes = JSON.parse(window.localStorage.getItem("fishes"));
        const updatedFishes = [...storedFishes,fish];
        window.localStorage.setItem("fishes",JSON.stringify(updatedFishes));
        setFishes(updatedFishes);
        //setFish(updatedFishes);
    }

    const nemo = () => {
        setFish(fish => {
            return {...fish,name:"Nemo",color:"Orange-White",aquariumId:"123"}
        });
    }

    return (
        <div className="tableFormContainer">
            <FishTable fishes={nemo}/>
            <FishForm />
        </div>
    );
}
