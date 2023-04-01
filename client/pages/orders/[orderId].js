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
    Center,
} from '@chakra-ui/react';
import { useRouter } from 'next/router';
import { useAppStates } from '../../lib/AppContext';
import NextLink from 'next/link';
import { useEffect, useState } from 'react';

function Order() {
    const { user, api } = useAppStates();
    const [redirectUrl, setRedirectUrl] = useState(null)
    const router = useRouter();

    if (!api || !user) return (
        <Center minH="2xl" w="full" py={5} justifyItems="center">
            <NextLink href="/login">
                <Button size="lg" colorScheme="purple">Please login first</Button>
            </NextLink>
        </Center>
    );

    useEffect(() => {

    }, [])

    return (
        <>
            <Box minH="70vh">

            </Box>
        </>
    )
}

Order.getLayout = function getLayout(page) {
    return <AppLayout title={"Your Orders"}>{page}</AppLayout>
}

export default Order
