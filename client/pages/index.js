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
} from '@chakra-ui/react'
import { useAppStates } from '../lib/AppContext'
import Item from '../components/Item'
import { useCallback, useEffect, useState } from 'react'
import { useRouter } from 'next/router'

function Home() {
    const { user, addToCart, api } = useAppStates()
    const router = useRouter();
    const [items, setItems] = useState([]);
    const [pageNumber, setPageNumber] = useState(1);
    const [pageSize, setPageSize] = useState(20);
    const [totalCount, setTotalCount] = useState(0);
    const [totalPages, setTotalPages] = useState(0);
    const [hasMore, setHasMore] = useState(true);
    const [fetching, setFetching] = useState(false);

    useEffect(() => {
        fetchData(pageNumber)
    }, [pageNumber])

    const fetchData = async (page) => {
        if (!hasMore) return;

        const data = await api.getMeals(page);

        setHasMore(data.nextPage)

        setItems([...items, ...data.items])
    }

    const onScroll = () => {
        const scrollTop = document.documentElement.scrollTop
        const scrollHeight = document.documentElement.scrollHeight
        const clientHeight = document.documentElement.clientHeight

        if (scrollTop + clientHeight >= scrollHeight) {
            setPageNumber(pageNumber + 1)
        }
    }

    useEffect(() => {
        window.addEventListener('scroll', onScroll)
        return () => window.removeEventListener('scroll', onScroll)
    }, [items])

    return (

        <Box py={5}
            minH={"2xl"}>
            {/*<Flex mb={5} width="full">*/}
            {/*    <Input />*/}
            {/*    <Select placeholder='Type' width="fit-content">*/}
            {/*        <option value='option1'>Option 1</option>*/}
            {/*        <option value='option2'>Option 2</option>*/}
            {/*    </Select>*/}
            {/*    <Select placeholder='Type' width="fit-content">*/}
            {/*        <option value='option1'>Option 1</option>*/}
            {/*        <option value='option2'>Option 2</option>*/}
            {/*    </Select>*/}
            {/*</Flex>*/}

            <SimpleGrid
                p={1}
                columns={[2, null, 4]}
                spacing={5}
            >
                {items.map((item, index) => <Item key={"meal-" + item["Id"]} obj={item}></Item>)}
            </SimpleGrid>

        </Box>

    )
}

Home.getLayout = function getLayout(page) {
    return <AppLayout>{page}</AppLayout>
}

export default Home
