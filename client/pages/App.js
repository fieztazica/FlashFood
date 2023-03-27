import './App.css'
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom'
import { header } from './components/shared/header'
import { Shop } from '.pages/Shop/shop'
import { Cart } from '.pages/Cart/cart'
function App() {
    return (
        <div className="App">
            <Router>
                <header />
                <Routes>
                    <Route path="/shop" element={<Shop />} />
                    <Route path="/cart" element={<Cart />} />
                </Routes>
            </Router>
        </div>
    )
}

export default App
