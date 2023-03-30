import { useToast } from '@chakra-ui/react';
import axios from 'axios';
import { useState, useEffect, createContext, useContext } from 'react'
import instanceApi, { tokenKey } from '.';

const AppContext = createContext();

export function AppContextProvider({ children }) {
    const toast = useToast();
    const [user, setUser] = useState(null);
    const [cart, setCart] = useState([]);
    const [api] = useState(() => instanceApi(axios.create({
        baseURL: `${process.env.apiBaseUrl}`,
    })));

    if (!api) return;

    useEffect(() => {
        api.setTokenToInstance(localStorage.getItem(tokenKey))
    }, [])

    useEffect(() => {
        getUserInfo()
    }, [user])

    function getUserInfo() {
        api.getUserInfo().then(u => {
            if (JSON.stringify(u) != JSON.stringify(user))
                setUser(u)
        }).catch((e) => {
            console.error(e)
            setUser(null)
        });
    }

    function logout() {
        api.logout();
        setUser(null);
    }

    async function login({ ...props }) {
        api.login({ ...props }).then(() => getUserInfo())
    }

    function addToCart(item) {
        setCart((a) => [...a, item]);
        toast({
            title: `Added ${item["Name"]} to your cart!`
        })
    }

    function removeFromCart(item) {
        setCart((a) => a.filter(x => x.id != item.id))
    }

    let sharedStates = {
        user, api, addToCart, removeFromCart, cart, logout, getUserInfo, login
    }

    return (
        <AppContext.Provider value={sharedStates}>
            {children}
        </AppContext.Provider>
    )
}

export function useAppStates() {
    return useContext(AppContext);
}
