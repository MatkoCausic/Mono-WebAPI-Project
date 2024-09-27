import React from 'react';

export default
function FishTable({fishes, updateFish, deleteFish}){
    return(
        <div id="tableContainer">
        <table>
          <thead>
            <tr>
              <th>Id</th>
              <th>Name</th>
              <th>Color</th>
              <th>IsAggressive</th>
              <th>AquariumId</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            {fishes && fishes.map(fish => (
            <tr>
              <td>{fish.id}</td>
              <td>{fish.name}</td>
              <td>{fish.color}</td>
              <td>{fish.isAggressive?"true":"false"}</td>
              <td>{fish.aquariumId}</td>
              <td>
                <button onClick={() => updateFish(fish.id)}>Update</button>
                <button onClick={() => deleteFish(fish.id)}>Delete</button>
              </td>
            </tr>
            ))}
          </tbody>
        </table>
      </div>
    );
}
