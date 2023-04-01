import AppLayout from '@/components/layouts/appLayout'
import { Box, Button, Heading, Image, Skeleton, Text, VStack, Card, CardBody, CardHeader, Center, Spinner } from '@chakra-ui/react'
import { useEffect, useState } from 'react';
import { useAppStates } from '../../lib/AppContext';
import NextLink from 'next/link';

function Orders() {
    const { user, api } = useAppStates();
    const [fetching, setFetching] = useState(false);
    const [orders, setOrders] = useState([]);

    useEffect(() => {
        const fetchData = async () => {
            try {
                setFetching(true)
                const data = await api.getOrders();
                if (data) {
                    setOrders(data)
                    console.log(data)
                    setFetching(false)
                }
            } catch (e) {
                setFetching(false)
                console.error(e);
            }
        };
        if (user)
            fetchData()
    }, [user])

    if (!user)
        return (
            <Center minH="2xl" w="full" py={5} justifyItems="center">
                <NextLink href="/login">
                    <Button size="lg" colorScheme="purple">Please login first</Button>
                </NextLink>
            </Center>
        );

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
                        variant="outline"
                        as={NextLink}
                        href={`/orders/${order.Id}`}
                    >
                        <Heading size="md">Order #{order.Id}</Heading>
                        <Text fontWeight="semibold">Total: {order.TotalMoney}</Text>
                        <Text>Status: {order.Status}</Text>
                        {order.Paid && <Text>Paid {order.Paid} at {order.PaidAt}</Text>}
                        <Text>Order placed at {new Date(order.OrderAt).toLocaleString()}</Text>
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
