import React from 'react';

export default
function FishForm({fish,handleChange,addFish}){
    return (
        <div id="formContainer">
            <form type="submit" onSubmit={addFish}>
                <label>Id</label>
                <input type="text" id="id" name="id" onChange={handleChange} />
                <label>Name</label>
                <input type="text" id="name" name="name" onChange={handleChange} />
                <label>Color</label>
                <input type="text" id="color" name="color" onChange={handleChange} />
                <label>Is fish aggressive</label>
                <input type="checkbox" id="isAggressive" name="isAggressive" onChange={handleChange} />
                <label>Aquarium Id</label>
                <input type="text" id="aquariumId" name="aquariumId" onChange={handleChange} />
                <input type="submit" value="Submit" />
            </form>
        </div>
    );
}
