import AppLayout from '@/components/layouts/appLayout'
import {
    Box,
    Button,
    Container,
    Grid,
    Heading,
    Text,
    Image,
    GridItem,
    Card,
    CardBody,
    CardHeader,
    Stack,
    CardFooter,
    Divider,
    Flex,
    ButtonGroup,
    AspectRatio,
} from '@chakra-ui/react'
import { useRouter } from 'next/router';
import { useAppStates } from '../../lib/AppContext'
import { publicInstance } from '../../lib/serverInstance'
import NextLink from 'next/link'
import { useEffect, useState } from 'react';

function Order() {
    const { addToCart, user } = useAppStates();
    const [redirectUrl, setRedirectUrl] = useState(null)
    const router = useRouter();

    return (
        <>
            <Box minH="70vh">
                
            </Box>
        </>
    )
}

//export async function getServerSideProps(context) {
//    const mealId = context.query["mealId"];
//    const publicApi = publicInstance();
//    const meal = await publicApi.getMeal(mealId);
//    return {
//        props: {
//            data: meal
//        },
//    }
//}

Order.getLayout = function getLayout(page) {
    return <AppLayout>{page}</AppLayout>
}

export default Order
