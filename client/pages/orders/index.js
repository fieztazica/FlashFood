import AppLayout from '@/components/layouts/appLayout'
import { Box, Button, Heading, Image, Skeleton, Text, VStack, Card, CardBody, CardHeader, Center, Spinner, Tabs, TabList, Tab, TabPanels, TabPanel, Select } from '@chakra-ui/react'
import { useEffect, useState } from 'react';
import { useAppStates } from '../../lib/AppContext';
import NextLink from 'next/link';

const statuses = ["ordered", "making", "ready", "canceled", "accepted"]
const colors = [undefined, "yellow.200", "green.200", "red.200", "orange.200"]

function Orders() {
    const { user, api } = useAppStates();
    const [fetching, setFetching] = useState(false);
    const [orders, setOrders] = useState([]);
    const [tabIndex, setTabIndex] = useState(0)

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
        <Box minH="70vh">
            <VStack spacing={8} align="stretch" my={5}>
                <Tabs display={["none", null, "block"]} onChange={(index) => setTabIndex(index)}>
                    <TabList>
                        {statuses.map((status, index) => <Tab key={status} bg={tabIndex == index && colors[index]}>{status}</Tab>)}
                    </TabList>
                </Tabs>
                <Select display={["block", null, "none"]} onChange={(ev) => setTabIndex(ev.target.value)} bg={colors[tabIndex]}>
                    {statuses.map((status, index) => <option key={status} value={index}>{status}</option>)}
                </Select>
                {orders.filter(x => x.Status == statuses[tabIndex]).sort((a, b) =>  Date.parse(b.OrderAt) - Date.parse(a.OrderAt)).map((order) => (
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
        </Box>
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
