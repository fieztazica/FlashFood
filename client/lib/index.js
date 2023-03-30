export const tokenKey = "flashfood_token";

export const controllers = {
    account: `/api/Account`,
    meal: `/api/Meals`,
    order: `/api/Orders`
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

    const getMeal = async (id) => {
        const { data } = await instance.get(`${controllers.meal}/Get/${id}`);
        return data;
    }

    const getMeals = async (pageNumber = 1, pageSize = 10) => {
        const { data } = await instance.get(`${controllers.meal}/Get?pageNumber=${pageNumber}&pageSize=${pageSize}`);
        return data;
    }

    const getOrders = async () => {
        const { data } = await instance.get(`${controllers.order}/Get?pageNumber=${pageNumber}&pageSize=${pageSize}`);
        return data;
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
        routes,
        getMeal,
        getMeals
    }
}
