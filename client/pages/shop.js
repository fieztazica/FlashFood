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
} from '@chakra-ui/react'
function Shop() {
    return (
        <>
            <Box
                padding={0}
                margin={0}
                boxSizing={'border-box'}
                fontFamily={['Rubik', 'sans-serif']}
                bg={'#fdf2e9'}
                padding={[4.8, 0, 9.6, 0]}
                borderbottom={'1px solid #eee'}
            >
                <Heading textAlign={'center'}>FlashFood Shop</Heading>
                <Grid
                    maxWidth={'100xl'}
                    margin={(0, 'auto')}
                    padding={(0, '3.2rem')}
                    gridTemplateColumns={'repeat(3, 2fr)'}
                    gap={'9.6rem'}
                    alignItems={'center'}
                >
                    <GridItem>
                        <Image
                            src={
                                'https://images.unsplash.com/photo-1623489254660-db5b367881d9?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=772&q=80'
                            }
                            alt={'asdas'}
                        ></Image>
                    </GridItem>
                    <GridItem>
                        <Image
                            src={
                                'https://images.unsplash.com/photo-1601924582970-9238bcb495d9?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=776&q=80'
                            }
                            alt={'asdas'}
                        ></Image>
                    </GridItem>
                    <GridItem>
                        <Image
                            src={
                                'https://images.unsplash.com/photo-1561758033-7e924f619b47?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=774&q=80'
                            }
                            alt={'asdas'}
                        ></Image>
                    </GridItem>
                    <GridItem>
                        <Image
                            src={
                                'https://images.unsplash.com/photo-1598679253544-2c97992403ea?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=774&q=80'
                            }
                            alt={'asdas'}
                        ></Image>
                    </GridItem>
                    <GridItem>
                        <Image
                            src={
                                'https://images.unsplash.com/photo-1624153064067-566cae78993d?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=774&q=80'
                            }
                            alt={'asdas'}
                        ></Image>
                    </GridItem>
                    <GridItem>
                        <Image
                            src={
                                'https://images.unsplash.com/photo-1618163633027-30a494d08cc5?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=774&q=80'
                            }
                            alt={'asdas'}
                        ></Image>
                    </GridItem>
                </Grid>
            </Box>
        </>
    )
}
Shop.getLayout = function getLayout(page) {
    return <AppLayout bg={'red'}>{page}</AppLayout>
}

export default Shop
