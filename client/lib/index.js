export const tokenKey = "flashfood_token";

export const controllers = {
    account: `/api/Account`,
}

/**
 *
 * @param {import("axios").AxiosInstance} instance
 * @returns
 */
export default function api(instance) {
    instance.defaults.headers.post["Content-Type"] = 'application/json';

    const login = async ({ email, password, rememberMe = false, ...props }) => {
        const { data } = await instance.post(`${controllers.account}/Login`, {
            "Email": email,
            "Password": password
        });
        const token = `${data}`

        localStorage.setItem(tokenKey, token);
        setTokenToInstance(token)
    }

    function setTokenToInstance(token) {
        instance.defaults.headers.common['Authorization'] = `Bearer ${token}`
    }

    function clearToken() {
        delete instance.defaults.headers.common['Authorization']
    }

    const register = async ({ email, password, confirmPassword, ...props }) => {
        const res = await instance.post(`${controllers.account}/Register`, {
            "Email": email,
            "Password": password,
            "ConfirmPassword": confirmPassword
        });
    }

    const logout = () => {
        localStorage.removeItem(tokenKey);
        clearToken();
    }

    const getUserInfo = async () => {
        const { data } = await instance.get(`${controllers.account}/UserInfo`)
        return data;
    }

    const addToCart = async (item) => {

    }

    const routes = {
        /**
         *
         * @param {string} redirectTo
         * @returns
         */
        login: (redirectTo = null) => redirectTo ? `/login?redirect=${redirectTo}` : `/login`,
        forbidden: () => '/forbidden',
    }

    return {
        login,
        getUserInfo,
        register,
        logout,
        clearToken,
        setTokenToInstance,
        routes
    }
}
