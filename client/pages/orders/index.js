import AppLayout from '@/components/layouts/appLayout'
import { Box, Button, Heading, Image, Skeleton, Text, VStack, Card, CardBody, CardHeader, Center, Spinner } from '@chakra-ui/react'
import { useEffect, useState } from 'react';
import { useAppStates } from '../../lib/AppContext';
import NextLink from 'next/link';

const fakeOrder = {
    Id: 0,
    OrderAt: new Date("09/09/2002").toLocaleString(),
    PaidAt: null,
    TotalMoney: 20000,
    Paid: null,
    Change: null,
    SellerId: 1
}

function Orders() {
    const { user, api } = useAppStates();
    const [fetching, setFetching] = useState(false);
    const [orders, setOrders] = useState(new Array(10).fill(fakeOrder));

    //if (!api || !user)
    //    return (
    //        <Center minH="2xl" w="full" py={5} justifyItems="center">
    //            <NextLink href="/login">
    //                <Button size="lg" colorScheme="purple">Please login first</Button>
    //            </NextLink>
    //        </Center>
    //    );

    //useEffect(() => {
    //    const fetchData = async () => {
    //        try {
    //            setFetching(true)
    //            //const data = await api.getMeals();
    //            const data = new Array(10).fill(fakeOrder)
    //            if (data) {
    //                setOrders(a => [...a, ...data])
    //                setFetching(false)
    //            }
    //        } catch (e) {
    //            setFetching(false)
    //            console.error(e);
    //        }
    //    };
    //    if (user)
    //        fetchData()
    //}, [user])

    if (!!fetching)
        return (
            <Center minH="2xl" w="full" py={5} justifyItems="center">
                <Spinner />
            </Center>
        )

    if (!fetching && !orders.length)
        return (
            <Center minH="2xl" w="full" py={5} justifyItems="center">
                No orders found.
            </Center>
        );

    return (
        <>
            <VStack spacing={8} align="stretch" my={5}>
                {orders.map((order) => (
                    <Card
                        key={order.Id}
                        p={4}
                        cursor={"pointer"}
                        transitionDuration="0.3s"
                        _hover={{ boxShadow: '1px 1px 1px 1px grey' }}
                        variant="outline">
                        <Heading size="md">Order #{order.Id}</Heading>
                        <Text fontWeight="semibold">Total: {order.TotalMoney}</Text>
                        <Text>Status: {order.Paid ? "Paid" : "Unpaid"}</Text>
                        {order.Paid && <Text>Paid at: {order.PaidAt}</Text>}
                        <Text>Order placed at: {order.OrderAt}</Text>
                        {order.Change && <Text>Change: {order.Change}</Text>}
                    </Card>
                ))}
            </VStack>
        </>
    )
}

Orders.getLayout = function getLayout(page) {
    return (
        <AppLayout title="Your Orders">
            {page}
        </AppLayout>
    )
}

export default Orders
