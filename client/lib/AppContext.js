import { useToast } from '@chakra-ui/react';
import axios from 'axios';
import { useState, useEffect, createContext, useContext } from 'react'
import instanceApi, { tokenKey } from '.';
import Cart from '../pages/cart';

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
        getUserCart()
        console.log(user)
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
        setCart([]);
    }

    async function login({ ...props }) {
        api.login({ ...props }).then(() => getUserInfo())
    }

    async function addToCart(item, amount = 1) {
        try {
            toast({
                title: `Adding ${item["Name"]}...`,
                status: "loading"
            })
            const data = await api.addToCart(item, amount)
            if (data) {
                setCart(data)
                const addedItem = data.find(x => x.MealId == item.Id)
                toast({
                    title: `Added ${addedItem.MealName}${addedItem.Amount > 1 ? ` (x${addedItem.Amount})` : ""} to your cart!`,
                    status: "success"
                })
            }
        } catch (e) {
            console.error(e)
            toast({
                title: "There is an error occured!",
                status: "error"
            })
        }
    }

    async function getUserCart() {
        try {
            const data = await api.getCart();
            if (data) {
                setCart(data);
            }
        } catch (e) {
            console.error(e)
        }
    }

    async function deleteCartItem(item) {
        try {
            toast({
                title: `Deleting ${item["MealName"]}...`,
                status: "loading"
            })
            const data = await api.deleteCartItem(item);
            if (data) {
                setCart(data);
                toast({
                    title: "Deleted!",
                    status: "success"
                })
            }
        } catch (e) {
            console.error(e)
            toast({
                title: "There is an error occured!",
                status: "error"
            })
        }
    }

    const action = {
        // Auth
        logout,
        getUserInfo,
        login,
        // Cart
        addToCart,
        deleteCartItem,
        getUserCart,
        setCart,
        // Order
    }

    let sharedStates = {
        api,
        action,
        cart,
        user,
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
