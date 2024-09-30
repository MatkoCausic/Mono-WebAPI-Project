import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import FishCrud from './FishCrud';
import reportWebVitals from './reportWebVitals';
import { BrowserRouter } from 'react-router-dom';

// const root = ReactDOM.createRoot(document.getElementById('root'));
// root.render(
//   <React.StrictMode>
//     <FishCrud />
//   </React.StrictMode>
// );

// reportWebVitals();

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<FishCrud />} />
      </Routes>
    </BrowserRouter>
  </React.StrictMode>
);

reportWebVitals();
