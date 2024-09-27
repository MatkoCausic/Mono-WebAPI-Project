import React from "react";
import FishTable from './FishTable.js';

export default
function FishForm(){
    return(
        <div id="formContainer">
            <form onsubmit="return false">
                <label>Id</label>
                <input type="text" id="id" autoComplete='on' value="1" required/>
                <label>Name</label>
                <input type="text" id="name" autoComplete='on' value="Leonardo" />
                <label>Color</label>
                <input type="text" id="color" value="Blue" />
                <label>IsAggressive</label>
                <input type="checkbox" id="isAggressive" value="off" />
                <label>AquariumId</label>
                <input type="text" id="aquariumId" value="123" requeired />
                <button onClick={getFormInput()}>Submit</button>
            </form>
        </div>
    );
}

function getFormInput(){
    const id = parseInt(document.getElementById("id").value);
    const name = document.getElementById("name").value;
    const color = document.getElementById("color").value;
    const isAggressive = document.getElementById("isAggressive").value;
    const aquariumId = document.getElementById("aquariumId").value;

    if(window.localStorage.getItem(id))
        return;

    
}
