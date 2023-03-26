import axios from 'axios';
import instanceApi from '.';

/**
 * 
 * @param {string} token
 * @returns
 */
export default function serverInstace(token) {
    const instance = axios.create({
        baseURL: `${process.env.apiBaseUrl}`
    });

    const api = instanceApi(instance);

    api.setTokenToInstance(token);

    return api;
}

