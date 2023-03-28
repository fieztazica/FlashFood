import { useToast } from '@chakra-ui/react';
import axios from 'axios';
import { useState, useEffect, createContext, useContext } from 'react'
import instanceApi, { tokenKey } from '.';

const AppContext = createContext();

export function AppContextProvider({ children }) {
    const toast = useToast();
    const [isAuthorized, setIsAuthorized] = useState(false);
    const [user, setUser] = useState(null);
    const [cart, setCart] = useState([]);
    const [instance] = useState(() => axios.create({
        baseURL: `${process.env.apiBaseUrl}`,
    }));
    const [api] = useState(() => instanceApi(instance));

    useEffect(() => {
        (async () => {
            api.setTokenToInstance(localStorage.getItem(tokenKey))
            api.getUserInfo().then(u => {
                setUser(u)
                setIsAuthorized(true)
            }).catch((e) => {
                console.error(e)
                setIsAuthorized(false)
            });
        })()
    }, [])

    useEffect(() => {
        console.log(user)
    }, [user])

    function addToCart(item) {
        setCart((a) => [...a, item]);
        toast({
            title: `Added ${item.id} to your cart!`
        })
    }

    function removeFromCart(item) {
        setCart((a) => a.filter(x => x.id != item.id))
    }

    let sharedStates = {
        isAuthorized, user, instance, api, addToCart, removeFromCart, cart
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
