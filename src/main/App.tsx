import HouseList from '../house/HouseList'
import './App.css'
import Header from './Header'

function App() {
  return (
    <div className='container'>
      <Header subtitle='List of houses on sale' />
      <HouseList />
    </div>
  )
}

export default App
