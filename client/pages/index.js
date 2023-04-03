import Head from 'next/head'
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
    useDisclosure,
    SimpleGrid,
    Input,
    Select,
    Flex,
    Card,
    CardBody,
    Center,
    Spinner,
} from '@chakra-ui/react'
import { useAppStates } from '../lib/AppContext'
import Item from '../components/Item'
import { useCallback, useEffect, useState } from 'react'
import { useRouter } from 'next/router'
import InfiniteScroll from 'react-infinite-scroll-component';

function Home() {
    const { user, action, api } = useAppStates()
    const [items, setItems] = useState([]);
    const [pageNumber, setPageNumber] = useState(1);
    const [hasMore, setHasMore] = useState(true);
    const [fetching, setFetching] = useState(false);

    useEffect(() => {
        fetchData()
    }, [])

    const fetchData = async () => {
        try {
            //if (!hasMore) return;
            setPageNumber(pageNumber + 1)
            setFetching(true)
            const data = await api.getMeals(pageNumber);

            if (data) {
                setHasMore(data.nextPage)
                setItems([...items, ...data.items])
            }
        } catch (e) {
            console.error(e)
        } finally {
            setFetching(false)
        }
    }

    if (!fetching && !items.length)
        return (
            <Center minH="2xl" w="full" py={5} justifyItems="center">
                No items found.
            </Center>
        )

    return (
        <Box
            py={5}
            minH={"2xl"}
        >
            <InfiniteScroll
                dataLength={items.length} //This is important field to render the next data
                next={() => fetchData()}
                hasMore={!!hasMore}
                loader={
                    <Center display={ !!fetching ? "flex" : "none" } w="full" py={5} justifyItems="center">
                        <Spinner />
                    </Center>
                }
            >
                <SimpleGrid
                    p={1}
                    columns={[2, 3, 4]}
                    spacing={5}
                >
                    {items.map((item, index) => <Item key={"meal-" + item["Id"]} obj={item}></Item>)}
                </SimpleGrid>
            </InfiniteScroll>
        </Box>

    )
}

Home.getLayout = function getLayout(page) {
    return <AppLayout>{page}</AppLayout>
}

export default Home
