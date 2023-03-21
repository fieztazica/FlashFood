import Head from 'next/head'
import Image from 'next/image'
import AppLayout from '@/components/layouts/appLayout'
import { Box, Button, getToken, Heading } from '@chakra-ui/react'
import { useEffect, useState } from 'react'
import { controllers, login } from '../lib'
import axios from 'axios'

function Login() {

    const [data, setData] = useState(null);

    useEffect(() => {
        login("dat09t@gmail.com", "P@ssw0rd", false);
    }, []);

    return (
        <>
            <Box>
                <Heading>
                    Abc
                </Heading>
                <Button>
                    nut
                </Button>
            </Box>
        </>
    )
}

Login.getLayout = function getLayout(page) {
    return (
        <AppLayout>
            {page}
        </AppLayout>
    )
}

/**
 * 
 * @param {import("next").GetServerSidePropsContext} ctx
 * @returns
 */
//export async function getServerSideProps(ctx) {
//    const res = await axios.get(`${controllers.account}/UserInfo`, {
//        headers: {
//            Authorization: `Bear ${getToken()}`
//        }
//    });

//    const data = await res.json();
//    // Pass data to the page via props
//    return { props: { data } };
//}

export default Login
