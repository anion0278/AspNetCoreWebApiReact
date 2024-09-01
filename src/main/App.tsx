import { BrowserRouter, Route, Routes } from 'react-router-dom'
import HouseList from '../house/HouseList'
import './App.css'
import Header from './Header'
import HouseDetail from '../house/HouseDetail'

function App() {
  return (
    <BrowserRouter>
      <div className='container'>
        <Header subtitle='List of houses on sale' />
        <Routes>
          <Route path="/" element={<HouseList/>}></Route>
          <Route path="/house/:id" element={<HouseDetail/>}></Route>
        </Routes>
      </div>
    </BrowserRouter>
  )
}

export default App;
