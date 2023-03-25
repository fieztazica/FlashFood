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
} from '@chakra-ui/react'

function Home() {
    return (
        <>
            <Box
                padding={0}
                margin={0}
                boxSizing={'border-box'}
                fontFamily={['Rubik', 'sans-serif']}
                bg={'#fdf2e9'}
                padding={[4.8, 0, 9.6, 0]}
                border-bottom={'1px solid #eee'}
            >
                <Grid
                    maxWidth={'130rem'}
                    margin={(0, 'auto')}
                    padding={(0, '3.2rem')}
                    gridTemplateColumns={'repeat(2, 1fr)'}
                    gap={'9.6rem'}
                    alignItems={'center'}
                >
                    <GridItem textAlign={'center'}>
                        <Heading
                            fontWeight={20}
                            color={'#333'}
                            letterSpacing={'-0.5px'}
                        >
                            A healthy meal delivered to your door, every single
                            day
                        </Heading>
                        <Text
                            color={'#333'}
                            lineHeight={1.6}
                            marginBottom={4.8}
                        >
                            The smart 365-days-per-year food subscription that
                            will make you eat healthy again. Tailored to your
                            personal tastes and nutritional needs.
                        </Text>
                    </GridItem>
                    <GridItem textAlign={'center'}>
                        <Image
                            src={
                                'https://images.unsplash.com/photo-1610963490387-0c08126eacf8?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=2535&q=80'
                            }
                            alt={'asdas'}
                        ></Image>
                    </GridItem>
                </Grid>
            </Box>
        </>
    )
}

Home.getLayout = function getLayout(page) {
    return <AppLayout bg={'red'}>{page}</AppLayout>
}

export default Home
