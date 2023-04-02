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
import { publicInstance } from '../../lib/instances'
import NextLink from 'next/link'
import { useEffect, useState } from 'react';

function Meal({ data }) {
    const { action, user } = useAppStates();
    const [redirectUrl, setRedirectUrl] = useState(null)
    const router = useRouter();
    const { addToCart } = action

    useEffect(() => {
        setRedirectUrl(window.location.href)
    }, [])

    return (
        <>
            <Box minH="70vh" py={5}>
                <Flex width="full" flexDirection={['column-reverse', null, "row"]}>
                        <Image borderRadius='md' boxSize='md' objectFit='cover' src={data["ImageURL"]} alt={`${data["Name"]} image`} width={["full", null, 4 / 10]} />
                    <Stack width="100%" px={[1, null, 5]} mb={[4, null, 0]} >
                        <Heading fontSize="2xl">
                            {data["Name"]}                        </Heading>
                        <Text >
                            {data["Price"]} (VND)
                        </Text>
                        <Text >
                            Left: {data["AmountLeft"]} ({data["Type"]})
                        </Text>
                    </Stack>

                </Flex>

                <ButtonGroup mt={5} width="full">
                    <Button size="lg" width="full" onClick={() => router.back()}>Back
                    </Button>
                    {!!user ? (<Button size="lg" colorScheme="purple" width="full" onClick={() => addToCart(data)}>
                        Add to cart
                    </Button>) : (<Button as={NextLink} size="lg" colorScheme="cyan" width="full" href={`/login?redirect=${encodeURIComponent(redirectUrl)}`}>
                        Login
                    </Button>)}


                </ButtonGroup>
            </Box>
        </>
    )
}

/**
 * 
 * @param {import("next").GetServerSidePropsContext} context
 * @returns
 */
export async function getServerSideProps(context) {
    const mealId = context.query["mealId"];
    const publicApi = publicInstance();
    const meal = await publicApi.getMeal(mealId);
    return {
        props: {
            data: meal
        },
    }
}

Meal.getLayout = function getLayout(page) {
    return <AppLayout>{page}</AppLayout>
}

export default Meal
