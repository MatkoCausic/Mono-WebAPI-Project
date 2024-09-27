import React from "react";

export default
function FishTable({fishes}){
    return (
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
                    {!fishes && tableMapping(fishes)}
                </tbody>
            </table>
        </div>
    );
}

function tableMapping({fishes}){
    return(
        fishes.map(fish => (
        <tr>
            <td>{fish.id}</td>
            <td>{fish.name}</td>
            <td>{fish.color}</td>
            <td>{fish.isAggressive}</td>
            <td>{fish.aquariumId}</td>
            <td>
                {/* <button value="Update" onClick={(fish) => {
                    
                }} />
                <button value="Delete" onClick={(fishId) => {
                    const storedFishes = JSON.parse(window.localStorage.getItem("fishes"));
                    const updatedFishes = storedFishes.filter(fish => fish.Id !== fishId);
                    window.localStorage.setItem("fishes",JSON.stringify(updatedFishes))
                }} /> */}
            </td>
        </tr>
        ))
    );
}
