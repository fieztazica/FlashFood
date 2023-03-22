import axios from 'axios';
import { useState, useEffect, createContext, useContext } from 'react'
import instanceApi, { tokenKey } from '.';

const AppContext = createContext();

export function AppContextProvider({ children }) {
    const [isAuthorized, setIsAuthorized] = useState(false);
    const [user, setUser] = useState(null);

    const instance = axios.create({
        baseURL: `${process.env.apiBaseUrl}`,
    });

    const api = instanceApi(instance);

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

    let sharedStates = {
        isAuthorized, user, instance, api
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